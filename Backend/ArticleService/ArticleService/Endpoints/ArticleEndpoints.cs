using ArticleService.Models.Request;
using ArticleService.Models.Response;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ArticleService.Endpoints;

public static class ArticleEndpoints
{
    public static void RegisterGetArticleEndpoints(this WebApplication app)
    {
        app.MapPost("/GetArticleByTitle", GetArticle);
    }

    private static async Task<Results<Ok<ArticleResponse>, ProblemHttpResult>> GetArticle(
        GetArticleByTitleRequest request)
    {
        return TypedResults.Ok(new ArticleResponse
        {
            Author = "Works",
            Title = "Works",
            Content = "Works",
            DatePublished = new DateOnly()
        });
    }
}