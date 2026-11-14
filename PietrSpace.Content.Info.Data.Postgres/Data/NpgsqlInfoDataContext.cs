using Microsoft.EntityFrameworkCore;
using PietrSpace.Content.Info.Data.Models.Entities;

namespace PietrSpace.Content.Info.Data.Postgres.Data;

public class NpgsqlInfoDataContext(DbContextOptions<NpgsqlInfoDataContext> options) : DbContext(options)
{
    public DbSet<InfoEntryEntity> InfoEntries { get; init; } = null!;
}
