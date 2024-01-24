using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Shared;

namespace Application.Books.Queries
{
    public class BooksSearchQueryHandler : IQueryHandler<BooksSearchQuery, IEnumerable<BookResponse>>
    {
        private readonly IBookRepository _bookRepository;

        public BooksSearchQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Result<IEnumerable<BookResponse>>> Handle(BooksSearchQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookRepository.BooksSearch(request.SearchKey!,
                request.PageNumber, request.PageSize, cancellationToken);

            var response = new List<BookResponse>();

            foreach (var item in result)
            {
                response.Add(new BookResponse
                {
                    Id = item.Id,
                    Title = item.Title,
                    Author = item.Author,
                    Description = item.Description,
                    PublishDate = item.PublishDate,
                    CoverBase64 = item.CoverBase64
                });
            }

            return response;
        }
    }
}
