using BookStore.Bookstore.Contracts;
using BookStore.Bookstore.Endpoints;

namespace BookStore.Data;

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

