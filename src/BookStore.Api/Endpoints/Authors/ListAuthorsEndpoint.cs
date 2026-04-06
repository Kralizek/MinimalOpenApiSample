using BookStore.Bookstore.Endpoints;

using Microsoft.AspNetCore.Http.HttpResults;

using Microsoft.EntityFrameworkCore;
using BookStore.Data;

namespace BookStore.Endpoints.Authors;

public class ListAuthorsEndpoint(BookstoreDbContext dbContext) : ListAuthorsEndpointBase
{
    public override async Task<Ok<string[]>> HandleAsync(CancellationToken cancellationToken)
    {
        var authors = await dbContext.Books
            .AsNoTracking()
            .Select(x => x.Author)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .OrderBy(x => x)
            .ToArrayAsync(cancellationToken);

        return TypedResults.Ok(authors);
    }
}

