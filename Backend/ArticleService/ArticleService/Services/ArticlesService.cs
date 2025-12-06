using ArticleService.Entities;
using ArticleService.Interfaces.Database;
using ArticleService.Interfaces.Repository;
using ArticleService.Interfaces.Services;
using ArticleService.Models.Request;
using ArticleService.Models.Response;

namespace ArticleService.Services;

public class ArticlesService : IArticlesService
{
    private readonly ILogger<ArticlesService> _logger;
    private readonly IArticlesRepository _articlesRepository;

    public ArticlesService(ILogger<ArticlesService> logger,
        IArticlesRepository articlesRepository)
    {
        _logger = logger;
        _articlesRepository = articlesRepository;
    }


    public async Task<List<ArticleResponse>> GetArticleByTitle(GetArticlesByTitleRequest request)
    {
        _logger.LogInformation("{Class}.{Method} started at {Time}",
            nameof(ArticlesService), nameof(GetArticleByTitle), DateTime.UtcNow);

        List<ArticleEntity> articleSearchResult = await _articlesRepository.GetArticlesByTitle(request.PossibleTitle);
        var response = new List<ArticleResponse>();

        foreach (var articleEntity in articleSearchResult)
        {
            response.Add(new ArticleResponse
            {
                Author = articleEntity.Author,
                Title = articleEntity.Title,
                DatePublished = articleEntity.DatePublished,
                Content = articleEntity.Content,
            });
        }

        _logger.LogInformation("{Class}.{Method} complete at {Time}",
            nameof(ArticlesService), nameof(GetArticleByTitle), DateTime.UtcNow);
        return response;
    }

    public async Task CreateArticle(CreateArticleRequest request)
    {
        _logger.LogInformation("{Class}.{Method} started at {Time}",
            nameof(ArticlesService), nameof(CreateArticle), DateTime.UtcNow);

        var article = new ArticleEntity
        {
            Author = request.Author,
            Content = request.Content,
            DatePublished = request.DatePublished,
            Title = request.Title,
            Id = Guid.NewGuid()
        };

        await _articlesRepository.CreateArticle(article);

        _logger.LogInformation("{Class}.{Method} complete at {Time}",
            nameof(ArticlesService), nameof(CreateArticle), DateTime.UtcNow);
    }
}