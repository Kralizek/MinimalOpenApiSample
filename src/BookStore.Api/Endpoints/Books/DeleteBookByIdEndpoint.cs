using BookStore.Bookstore.Endpoints;

using Microsoft.AspNetCore.Http.HttpResults;

using Microsoft.EntityFrameworkCore;
using BookStore.Data;

namespace BookStore.Endpoints.Books;

public class DeleteBookByIdEndpoint(BookstoreDbContext dbContext) : DeleteBookByIdEndpointBase
{
    public override async Task<Results<NoContent, NotFound>> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await dbContext.Books
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (book is null)
        {
            return TypedResults.NotFound();
        }

        dbContext.Books.Remove(book);
        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}

