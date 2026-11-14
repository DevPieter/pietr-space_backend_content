using Microsoft.EntityFrameworkCore;
using PietrSpace.Content.Quotes.Data.Models.Entities;

namespace PietrSpace.Content.Quotes.Data.Postgres.Data;

public class NpgsqlQuoteDataContext(DbContextOptions<NpgsqlQuoteDataContext> options) : DbContext(options)
{
    public DbSet<QuoteEntity> Quotes { get; init; } = null!;
    public DbSet<QuoteCollectionEntity> QuoteCollections { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO - yes
    }
}