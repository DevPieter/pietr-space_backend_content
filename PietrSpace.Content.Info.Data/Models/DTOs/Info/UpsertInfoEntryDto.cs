using System.Text.Json;

namespace PietrSpace.Content.Info.Data.Models.DTOs.Info;

public record UpsertInfoEntryDto(
    string Key,
    JsonElement Value
);