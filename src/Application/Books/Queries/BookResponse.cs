namespace Application.Books.Queries;

public class BookResponse
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; }
    public string CoverBase64 { get; set; } = string.Empty;
}
