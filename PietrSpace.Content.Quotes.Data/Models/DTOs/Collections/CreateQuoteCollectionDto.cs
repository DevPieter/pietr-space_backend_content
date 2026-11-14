namespace PietrSpace.Content.Quotes.Data.Models.DTOs.Collections;

public record CreateQuoteCollectionDto(
    Guid Id,
    Guid ExportId,
    string Name,
    string? Description,
    bool IsPublished
);