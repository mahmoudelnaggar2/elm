using Application.Abstractions.Messaging;

namespace Application.Books.Queries;

public sealed record BooksSearchQuery(string? SearchKey, int PageNumber, int PageSize) : IQuery<IEnumerable<BookResponse>>;
