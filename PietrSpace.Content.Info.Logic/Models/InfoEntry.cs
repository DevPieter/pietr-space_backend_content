using System.Text.Json;

namespace PietrSpace.Content.Info.Logic.Models;

public record InfoEntry(
    string Key,
    JsonElement Value
);