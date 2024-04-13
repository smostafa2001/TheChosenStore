namespace BlogManagement.Application.Contracts.ArticleAggregate;

public class ArticleViewModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Picture { get; set; }
    public string PublishDate { get; set; }
    public long CategoryId { get; set; }
    public string Category { get; set; }
}
