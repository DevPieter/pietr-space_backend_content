namespace PietrSpace.Content.Quotes.Data.Models.DTOs.Quotes;

public record CreateQuoteDto(
    Guid Id,
    Guid ExportId,
    Guid? QuoteCollectionId,
    string Text,
    string? HtmlText,
    string? Author,
    string? Credits,
    bool IsPublished
);