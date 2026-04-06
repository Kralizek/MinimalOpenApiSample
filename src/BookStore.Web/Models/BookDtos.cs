namespace BookStore.Web.Models;

public record BookDto(
    Guid Id,
    string Title,
    string Author,
    double Price,
    string? Isbn,
    IReadOnlyList<string>? Categories);

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

    public string[] Categories { get; set; } = [];
}
