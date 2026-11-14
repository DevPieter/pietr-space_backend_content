using PietrSpace.Common.Results;
using PietrSpace.Content.Quotes.Data.Models.DTOs.Collections;
using PietrSpace.Content.Quotes.Data.Models.DTOs.Quotes;
using PietrSpace.Content.Quotes.Data.Models.Entities;
using PietrSpace.Content.Quotes.Logic.Models.Domain;

namespace PietrSpace.Content.Quotes.Logic.Models;

public sealed class QuoteCollection : IdDomainEntity
{
    public Guid ExportId { get; private init; }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public bool IsPublished { get; private set; }

    private QuoteCollection(
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

        SetName(name);
        SetDescription(description);
        SetPublished(isPublished);

        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public QuoteCollection(
        QuoteCollectionEntity entity
    ) : this(
        entity.Id,
        entity.ExportId,
        entity.Name,
        entity.Description,
        entity.IsPublished,
        entity.CreatedAt,
        entity.UpdatedAt
    )
    {
    }

    public QuoteCollection SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
        Name = ClampString(name, 255);
        return this;
    }

    public QuoteCollection SetDescription(string? description)
    {
        Description = description is null ? null : ClampString(description, 2000);
        return this;
    }

    public QuoteCollection SetPublished(bool published)
    {
        IsPublished = published;
        return this;
    }

    public static Result ValidateCreate(CreateQuoteCollectionDto dto)
    {
        return Result.Success();
    }
}