using Domain.Shared;
using Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Books.Queries;
using System.Threading;

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
        [FromQuery] string? searchKey,
        [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new BooksSearchQuery(searchKey, pageNumber, pageSize);

        Result<IEnumerable<BookResponse>> response = await Sender.Send(query);

        return response.IsSuccess
            ? Ok(response.Value)
            : NotFound(response.Error);
    }
}
