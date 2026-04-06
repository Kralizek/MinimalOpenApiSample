using BookstoreApi.Bookstore.Endpoints;
using BookstoreApi.Data;

using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Http.HttpResults;

namespace BookstoreApi.Endpoints;

public class SearchBooksEndpoint(BookstoreDbContext dbContext) : SearchBooksEndpointBase
{
    public override async Task<Ok<OkResponse>> HandleAsync(Parameters parameters, CancellationToken cancellationToken)
    {
        var query = dbContext.Books.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Query))
        {
            query = query.Where(x =>
                EF.Functions.Like(x.Title, $"%{parameters.Query}%") ||
                EF.Functions.Like(x.Author, $"%{parameters.Query}%") ||
                (x.Isbn != null && EF.Functions.Like(x.Isbn, $"%{parameters.Query}%")));
        }

        if (!string.IsNullOrWhiteSpace(parameters.Author))
        {
            query = query.Where(x => EF.Functions.Like(x.Author, $"%{parameters.Author}%"));
        }

        if (parameters.Category is not null)
        {
            var token = parameters.Category.ToString();
            query = query.Where(x =>
                EF.Functions.Like(x.Categories, token) ||
                EF.Functions.Like(x.Categories, $"{token},%") ||
                EF.Functions.Like(x.Categories, $"%,{token},%") ||
                EF.Functions.Like(x.Categories, $"%,{token}"));
        }

        var page = parameters.Page is null or <= 0 ? 1 : parameters.Page.Value;
        var pageSize = parameters.PageSize is null or <= 0 ? 25 : parameters.PageSize.Value;

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(x => x.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => x.ToContract())
            .ToArrayAsync(cancellationToken);

        return TypedResults.Ok(new OkResponse
        {
            TotalCount = totalCount,
            Items = items
        });
    }
}
