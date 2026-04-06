using BookStore.Bookstore.Contracts;
using BookStore.Bookstore.Endpoints;

using Microsoft.AspNetCore.Http.HttpResults;

namespace BookStore.Endpoints.Categories;

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

