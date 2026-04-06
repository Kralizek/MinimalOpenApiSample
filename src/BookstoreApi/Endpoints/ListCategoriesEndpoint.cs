using BookstoreApi.Bookstore.Contracts;
using BookstoreApi.Bookstore.Endpoints;

using Microsoft.AspNetCore.Http.HttpResults;

namespace BookstoreApi.Endpoints;

public class ListCategoriesEndpoint : ListCategoriesEndpointBase
{
    public override async Task<Ok<Category[]>> HandleAsync(CancellationToken cancellationToken)
    {
        return TypedResults.Ok<Category[]>([
            Category.Fantasy,
            Category.SciFi,
            Category.NonFiction,
            Category.Fiction,
        ]);
    }
}
