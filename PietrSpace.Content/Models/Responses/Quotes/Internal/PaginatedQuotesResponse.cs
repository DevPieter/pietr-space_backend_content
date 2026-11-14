using PietrSpace.Common.Requests.Queries;
using PietrSpace.Common.Responses;
using PietrSpace.Content.Quotes.Logic.Models;

namespace PietrSpace.Content.Models.Responses.Quotes.Internal;

public record PaginatedQuotesResponse(
    int TotalItems,
    int TotalPages,
    SearchQuery? SearchQuery,
    PaginationQuery? PaginationQuery,
    List<QuoteResponse> Items
) : Paginated<QuoteResponse>(
    TotalItems,
    TotalPages,
    SearchQuery,
    PaginationQuery,
    Items
)
{
    public PaginatedQuotesResponse(
        Paginated<Quote> paginatedQuotes,
        PaginationQuery? PaginationQuery,
        SearchQuery? SearchQuery
    ) : this(
        paginatedQuotes.TotalItems,
        paginatedQuotes.TotalPages,
        SearchQuery,
        PaginationQuery,
        paginatedQuotes.Items.Select(q => new QuoteResponse(
            q.Id,
            q.ExportId,
            q.QuoteCollectionId,
            q.Text,
            q.HtmlText,
            q.Author,
            q.Credits,
            q.IsPublished
        )).ToList()
    )
    {
    }
}