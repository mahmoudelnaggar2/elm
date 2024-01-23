using Domain.Shared;
using Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Books.Queries;

namespace Presentation.Controllers;

[Route("api/books")]
public sealed class BooksController : ApiController
{
    public BooksController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet()]
    public async Task<IActionResult> BooksSearch(
        string? searchKey,
        CancellationToken cancellationToken)
    {
        var query = new BooksSearchQuery(searchKey);

        Result<IEnumerable<BookResponse>> response = await Sender.Send(
            query,
            cancellationToken);

        return response.IsSuccess
            ? Ok(response.Value)
            : NotFound(response.Error);
    }
}
