namespace PietrSpace.Content.Models.Responses.Quotes.Public;

public record PublicQuoteResponse(
    string Text,
    string? HtmlText,
    string? Author,
    string? Credits
);