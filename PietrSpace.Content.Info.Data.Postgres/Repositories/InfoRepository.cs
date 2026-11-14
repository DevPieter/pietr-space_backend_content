using Microsoft.EntityFrameworkCore;
using Npgsql;
using PietrSpace.Content.Info.Data.Models.DTOs.Info;
using PietrSpace.Content.Info.Data.Models.Entities;
using PietrSpace.Content.Info.Data.Postgres.Data;
using PietrSpace.Content.Info.Data.Repositories;

namespace PietrSpace.Content.Info.Data.Postgres.Repositories;

public class InfoRepository(NpgsqlInfoDataContext context) : IInfoRepository
{
    public Task<InfoEntryEntity?> GetInfoEntryAsync(string key) => context.InfoEntries
        .AsNoTracking()
        .SingleOrDefaultAsync(entry => entry.Key == key);

    public async Task UpsertInfoEntryAsync(UpsertInfoEntryDto dto)
    {
        if (await UpdateExistingEntryAsync(dto) > 0) return;

        var entry = context.InfoEntries.Add(new InfoEntryEntity
        {
            Key = dto.Key,
            Value = dto.Value
        });

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation })
        {
            // Another request inserted this key after the initial update.
            if (await UpdateExistingEntryAsync(dto) == 0) throw;
        }
        finally
        {
            entry.State = EntityState.Detached;
        }
    }

    private Task<int> UpdateExistingEntryAsync(UpsertInfoEntryDto dto) => context.InfoEntries
        .Where(entry => entry.Key == dto.Key)
        .ExecuteUpdateAsync(setters => setters.SetProperty(entry => entry.Value, dto.Value));
}
