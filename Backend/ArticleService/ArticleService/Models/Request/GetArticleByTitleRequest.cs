namespace ArticleService.Models.Request;

public record GetArticleByTitleRequest
{
    public required string PossibleTitle { get; set; }
}