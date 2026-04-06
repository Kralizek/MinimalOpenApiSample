using Microsoft.EntityFrameworkCore;

namespace BookStore.Data;

public class BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : DbContext(options)
{
    public DbSet<BookRecord> Books => Set<BookRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookRecord>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Author).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Isbn).HasMaxLength(17);
            entity.Property(x => x.Price).IsRequired();
            entity.Property(x => x.Categories).HasDefaultValue(string.Empty).IsRequired();
        });
    }
}

public class BookRecord
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Author { get; set; }

    public double Price { get; set; }

    public string? Isbn { get; set; }

    public string Categories { get; set; } = string.Empty;
}
