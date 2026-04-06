using BookstoreApi.Bookstore.Contracts;
using BookstoreApi.Bookstore.Endpoints;
using BookstoreApi.Data;

using Microsoft.AspNetCore.Http.HttpResults;

using Microsoft.EntityFrameworkCore;

namespace BookstoreApi.Endpoints;

public class UpdateBookByIdEndpoint(BookstoreDbContext dbContext) : UpdateBookByIdEndpointBase
{
    public override async Task<Results<Ok<Book>, NotFound>> HandleAsync(Guid id, Request request, CancellationToken cancellationToken)
    {
        var book = await dbContext.Books
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (book is null)
        {
            return TypedResults.NotFound();
        }

        book.ApplyUpdate(request);

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok(book.ToContract());
    }
}
