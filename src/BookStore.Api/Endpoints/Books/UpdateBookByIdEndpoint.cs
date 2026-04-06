using BookStore.Bookstore.Contracts;
using BookStore.Bookstore.Endpoints;

using Microsoft.AspNetCore.Http.HttpResults;

using Microsoft.EntityFrameworkCore;
using BookStore.Data;

namespace BookStore.Endpoints.Books;

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

        ApplyUpdate(book, request);

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok(book.ToContract());
    }

    private static void ApplyUpdate(BookRecord target, Request request)
    {
        if (request.Title is not null)
        {
            target.Title = request.Title;
        }

        if (request.Author is not null)
        {
            target.Author = request.Author;
        }

        if (request.Price is not null)
        {
            target.Price = request.Price.Value;
        }

        if (request.Isbn is not null)
        {
            target.Isbn = request.Isbn;
        }

        if (request.Categories is not null)
        {
            target.Categories = BookMappings.SerializeCategories(request.Categories);
        }
    }
}

