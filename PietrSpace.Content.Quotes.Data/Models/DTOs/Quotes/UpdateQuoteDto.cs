namespace PietrSpace.Content.Quotes.Data.Models.DTOs.Quotes;

public record UpdateQuoteDto(
    Guid? QuoteCollectionId,
    string Text,
    string? HtmlText,
    string? Author,
    string? Credits,
    bool IsPublished
);