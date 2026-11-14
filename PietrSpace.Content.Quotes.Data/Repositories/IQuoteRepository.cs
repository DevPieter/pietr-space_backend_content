using PietrSpace.Common.Requests.Queries;
using PietrSpace.Common.Responses;
using PietrSpace.Content.Quotes.Data.Models.DTOs.Collections;
using PietrSpace.Content.Quotes.Data.Models.DTOs.Quotes;
using PietrSpace.Content.Quotes.Data.Models.Entities;

namespace PietrSpace.Content.Quotes.Data.Repositories;

public interface IQuoteRepository
{
    Task<QuoteEntity> CreateQuoteAsync(
        CreateQuoteDto dto
    );

    Task<QuoteCollectionEntity> CreateCollectionAsync(
        CreateQuoteCollectionDto dto
    );

    Task<bool> DoesCollectionExistAsync(
        Guid id
    );

    Task<Paginated<QuoteEntity>> GetQuotesAsync(
        PaginationQuery pagination,
        SearchQuery search,
        bool includeCollection = false
    );

    Task<List<QuoteEntity>> GetRandomQuotesAsync(
        int count
    );

    Task<QuoteEntity?> UpdateQuoteAsync(
        Guid id,
        UpdateQuoteDto dto
    );

    Task<QuoteCollectionEntity?> UpdateCollectionAsync(
        Guid id,
        UpdateQuoteCollectionDto dto
    );
}