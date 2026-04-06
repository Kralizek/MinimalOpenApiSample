using BookStore.Bookstore.Contracts;
using BookStore.Bookstore.Endpoints;

using Microsoft.AspNetCore.Http.HttpResults;

using Microsoft.EntityFrameworkCore;
using BookStore.Data;

namespace BookStore.Endpoints.Books;

public class GetBookByIdEndpoint(BookstoreDbContext dbContext) : GetBookByIdEndpointBase
{
    public override async Task<Results<Ok<Book>, NotFound>> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await dbContext.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (book is null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(book.ToContract());
    }
}

