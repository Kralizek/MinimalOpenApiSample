using BookStore.Bookstore.Contracts;
using BookStore.Bookstore.Endpoints;
using BookStore.Data;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Endpoints.Books;

public class CreateBookEndpoint(BookstoreDbContext dbContext) : CreateBookEndpointBase
{
    public override async Task<Created<Book>> HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            Categories = request.Categories,
            Price = request.Price,
            Isbn = request.Isbn,
        };

        dbContext.Books.Add(book.ToRecord());
        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.Created($"/books/{book.Id}", book);
    }
}