using System.ComponentModel.DataAnnotations;

namespace PietrSpace.Content.Quotes.Data.Models.Entities;

public class QuoteCollectionEntity
{
    public Guid Id { get; private init; }
    public Guid ExportId { get; private init; }

    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [MaxLength(2000)]
    public string? Description { get; set; }

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; private init; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<QuoteEntity> Quotes { get; set; } = [];

    private QuoteCollectionEntity()
    {
    }

    public QuoteCollectionEntity(
        Guid id,
        Guid exportId,
        string name,
        string? description,
        bool isPublished,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        Id = id;
        ExportId = exportId;
        Name = name;
        Description = description;
        IsPublished = isPublished;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}