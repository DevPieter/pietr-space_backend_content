using System.Text.Json;
using PietrSpace.Common.Results;
using PietrSpace.Content.Info.Data.Models.DTOs.Info;
using PietrSpace.Content.Info.Data.Models.Entities;
using PietrSpace.Content.Info.Data.Repositories;
using PietrSpace.Content.Info.Logic.Models;

namespace PietrSpace.Content.Info.Logic.Services;

public class InfoService(IInfoRepository repository)
{
    public async Task<ResultObject<InfoEntry>> GetInfoEntryAsync(string key)
    {
        var entity = await repository.GetInfoEntryAsync(key);
        if (entity is null) return ResultObject<InfoEntry>.NotFound("Info entry not found.");

        return ResultObject<InfoEntry>.Success(new InfoEntry(entity.Key, entity.Value));
    }

    public async Task<Result> UpsertInfoEntryAsync(string key, JsonElement value)
    {
        if (string.IsNullOrWhiteSpace(key) || key.Length > 250) return Result.Validation($"Key must contain 1 to 250 characters.");
        if (value.ValueKind != JsonValueKind.Object) return Result.Validation("Value must be a JSON object.");

        await repository.UpsertInfoEntryAsync(new UpsertInfoEntryDto(key, value));
        return Result.Success();
    }
}