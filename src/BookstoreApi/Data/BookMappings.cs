using BookstoreApi.Bookstore.Contracts;
using BookstoreApi.Bookstore.Endpoints;

namespace BookstoreApi.Data;

public static class BookMappings
{
    public static Book ToContract(this BookRecord source)
    {
        return new Book
        {
            Id = source.Id,
            Title = source.Title,
            Author = source.Author,
            Price = source.Price,
            Isbn = source.Isbn,
            Categories = ParseCategories(source.Categories),
        };
    }

    public static BookRecord ToRecord(this Book source)
    {
        return new BookRecord
        {
            Id = source.Id,
            Title = source.Title,
            Author = source.Author,
            Price = source.Price,
            Isbn = source.Isbn,
            Categories = SerializeCategories(source.Categories),
        };
    }

    public static void ApplyUpdate(this BookRecord target, UpdateBookByIdEndpointBase.Request request)
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
            target.Categories = SerializeCategories(request.Categories);
        }
    }

    public static string SerializeCategories(IReadOnlyList<Category>? categories)
    {
        if (categories is null || categories.Count == 0)
        {
            return string.Empty;
        }

        return string.Join(',', categories);
    }

    public static Category[] ParseCategories(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return [];
        }

        return raw
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(static value => Enum.TryParse<Category>(value, ignoreCase: true, out var category)
                ? category
                : (Category?)null)
            .Where(static category => category.HasValue)
            .Select(static category => category!.Value)
            .ToArray();
    }
}
