using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace PietrSpace.Content.Info.Data.Models.Entities;

public class InfoEntryEntity
{
    [Key]
    public string Key { get; init; } = null!;

    public JsonElement Value { get; init; }
}