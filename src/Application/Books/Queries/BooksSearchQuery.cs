using Application.Abstractions.Messaging;

namespace Application.Books.Queries;

public sealed record BooksSearchQuery(string? SearchKey) : IQuery<IEnumerable<BookResponse>>;
