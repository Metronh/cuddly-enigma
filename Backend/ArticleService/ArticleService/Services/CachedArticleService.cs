using ArticleService.Interfaces.Services;
using ArticleService.Models.Request;
using ArticleService.Models.Response;
using Microsoft.Extensions.Caching.Memory;

namespace ArticleService.Services;

public class CachedArticleService : IArticlesService
{
    private readonly IArticlesService _articlesService;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<CachedArticleService> _logger;

    public CachedArticleService(IArticlesService articlesService, IMemoryCache memoryCache,
        ILogger<CachedArticleService> logger)
    {
        _articlesService = articlesService;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public async Task<List<ArticleResponse>> GetArticleByTitle(GetArticlesByTitleRequest request)
    {
        _logger.LogInformation("{Class}.{Method} started at {Time}",
            nameof(CachedArticleService), nameof(GetArticleByTitle), DateTime.UtcNow);
        
        List<ArticleResponse> result = await _memoryCache.GetOrCreateAsync(request.PossibleTitle,
            async entry => await _articlesService.GetArticleByTitle(request));

        _logger.LogInformation("{Class}.{Method} completed at {Time}",
            nameof(CachedArticleService), nameof(GetArticleByTitle), DateTime.UtcNow);
        return result;
    }

    public async Task CreateArticle(CreateArticleRequest request)
    {
        await _articlesService.CreateArticle(request);
    }
}