namespace PietrSpace.Content.Quotes.Logic.Models.Args.Quotes;

public record CreateQuoteArg(
    Guid? QuoteCollectionId,
    string Text,
    string? HtmlText,
    string? Author,
    string? Credits,
    bool IsPublished
);