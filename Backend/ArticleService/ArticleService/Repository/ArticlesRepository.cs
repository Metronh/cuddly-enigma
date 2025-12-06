using ArticleService.Entities;
using ArticleService.Interfaces.Database;
using ArticleService.Interfaces.Repository;
using MongoDB.Driver;

namespace ArticleService.Repository;

public class ArticlesRepository : IArticlesRepository
{
    private readonly IMongoDbConnectionFactory _mongoDbConnectionFactory;
    private readonly ILogger<ArticlesRepository> _logger;

    public ArticlesRepository(IMongoDbConnectionFactory mongoDbConnectionFactory, ILogger<ArticlesRepository> logger)
    {
        _mongoDbConnectionFactory = mongoDbConnectionFactory;
        _logger = logger;
    }

    public async Task<List<ArticleEntity>> GetArticlesByTitle(string possibleTitle)
    {
        _logger.LogInformation("{Class}.{Method} started at {Time}",
            nameof(ArticlesRepository), nameof(GetArticlesByTitle), DateTime.UtcNow);
        var collection = _mongoDbConnectionFactory.GetCollection();
        
        List<ArticleEntity> articleSearchResult =
            await collection.Find(a => a.Title.Equals(possibleTitle)).ToListAsync();
        
        _logger.LogInformation("{Class}.{Method} completed at {Time}",
            nameof(ArticlesRepository), nameof(GetArticlesByTitle), DateTime.UtcNow);
        return articleSearchResult;
    }

    public async Task CreateArticle(ArticleEntity article)
    {
        _logger.LogInformation("{Class}.{Method} started at {Time}",
            nameof(ArticlesRepository), nameof(CreateArticle), DateTime.UtcNow);

        var collection = _mongoDbConnectionFactory.GetCollection();
        
        await collection.InsertOneAsync(article);
        
        
        _logger.LogInformation("{Class}.{Method} completed at {Time}",
            nameof(ArticlesRepository), nameof(GetArticlesByTitle), DateTime.UtcNow);
    }
}