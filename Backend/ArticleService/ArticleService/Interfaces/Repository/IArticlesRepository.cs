using ArticleService.Entities;

namespace ArticleService.Interfaces.Repository;

public interface IArticlesRepository
{
    public Task<List<ArticleEntity>> GetArticlesByTitle(string possibleTitle);

    public Task CreateArticle(ArticleEntity article);
}