namespace PietrSpace.Content.Models.Requests.Quotes;

public record CreateQuoteRequest(
    Guid? QuoteCollectionId,
    string Text,
    string? HtmlText,
    string? Author,
    string? Credits,
    bool IsPublished
);