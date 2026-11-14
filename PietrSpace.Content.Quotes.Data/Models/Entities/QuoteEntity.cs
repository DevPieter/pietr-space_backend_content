using System.ComponentModel.DataAnnotations;

namespace PietrSpace.Content.Quotes.Data.Models.Entities;

public class QuoteEntity
{
    public Guid Id { get; private init; }
    public Guid ExportId { get; private init; }

    public Guid? QuoteCollectionId { get; set; }
    public QuoteCollectionEntity? QuoteCollection { get; set; }

    [MaxLength(2000)]
    public string Text { get; set; } = null!;

    [MaxLength(2000)]
    public string? HtmlText { get; set; }

    [MaxLength(255)]
    public string? Author { get; set; }

    [MaxLength(255)]
    public string? Credits { get; set; }

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; private init; }
    public DateTime UpdatedAt { get; set; }

    private QuoteEntity()
    {
    }

    public QuoteEntity(
        Guid id,
        Guid exportId,
        Guid? quoteCollectionId,
        string text,
        string? htmlText,
        string? author,
        string? credits,
        bool isPublished,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        Id = id;
        ExportId = exportId;
        QuoteCollectionId = quoteCollectionId;
        Text = text;
        HtmlText = htmlText;
        Author = author;
        Credits = credits;
        IsPublished = isPublished;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}