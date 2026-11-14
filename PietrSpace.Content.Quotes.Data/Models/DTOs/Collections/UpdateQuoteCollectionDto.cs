namespace PietrSpace.Content.Quotes.Data.Models.DTOs.Collections;

public record UpdateQuoteCollectionDto(
    string Name,
    string? Description,
    bool IsPublished
);