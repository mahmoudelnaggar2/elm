using Domain.Primitives;

namespace Domain.Entities
{
    public sealed class Book : Entity
    {
        public Book(long id, string bookTitle, string bookDescription, string author
            , DateTime publishDate, string coverBase64) : base(id)
        {
            Title = bookTitle;
            Description = bookDescription;
            Author = author;
            PublishDate = publishDate;
            CoverBase64 = coverBase64;
        }

        private Book() { }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string CoverBase64 { get; set; }
    }
}
