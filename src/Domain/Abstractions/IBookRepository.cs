using Domain.Entities;

namespace Domain.Abstractions;

public interface IBookRepository
{
    Task<IEnumerable<Book>> BooksSearch(string serachKey, CancellationToken cancellationToken = default);
}
