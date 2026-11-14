namespace PietrSpace.Content.Models.Responses.Quotes.Internal;

public record QuoteResponse(
    Guid Id,
    Guid ExportId,
    Guid? QuoteCollectionId,
    string Text,
    string? HtmlText,
    string? Author,
    string? Credits,
    bool IsPublished
);