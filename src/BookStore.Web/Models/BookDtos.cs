using System.Text.Json.Serialization;

namespace BookStore.Web.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Category
{
    Fiction,

    NonFiction,

    SciFi,

    Fantasy
}

public record BookDto(
    Guid Id,
    string Title,
    string Author,
    double Price,
    string? Isbn,
    IReadOnlyList<Category>? Categories);

public class SearchBooksResponse
{
    public int TotalCount { get; set; }

    public BookDto[] Items { get; set; } = [];
}

public class CreateBookRequest
{
    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public double Price { get; set; }

    public string? Isbn { get; set; }

    public Category[] Categories { get; set; } = [];
}

public class UpdateBookRequest
{
    public string? Title { get; set; }

    public string? Author { get; set; }

    public double? Price { get; set; }

    public string? Isbn { get; set; }

    public Category[]? Categories { get; set; }
}
