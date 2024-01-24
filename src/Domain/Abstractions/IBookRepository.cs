using Domain.Entities;

namespace Domain.Abstractions;

public interface IBookRepository
{
    Task<IEnumerable<Book>> BooksSearch(string serachKey, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
