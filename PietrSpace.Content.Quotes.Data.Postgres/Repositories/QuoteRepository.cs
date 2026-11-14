using Microsoft.EntityFrameworkCore;
using PietrSpace.Common.Requests.Queries;
using PietrSpace.Common.Responses;
using PietrSpace.Content.Quotes.Data.Models.DTOs.Collections;
using PietrSpace.Content.Quotes.Data.Models.DTOs.Quotes;
using PietrSpace.Content.Quotes.Data.Models.Entities;
using PietrSpace.Content.Quotes.Data.Postgres.Data;
using PietrSpace.Content.Quotes.Data.Repositories;

namespace PietrSpace.Content.Quotes.Data.Postgres.Repositories;

public class QuoteRepository(NpgsqlQuoteDataContext context) : IQuoteRepository
{
    public async Task<QuoteEntity> CreateQuoteAsync(
        CreateQuoteDto dto
    )
    {
        var entity = new QuoteEntity(
            id: dto.Id,
            exportId: dto.ExportId,
            quoteCollectionId: dto.QuoteCollectionId,
            text: dto.Text,
            htmlText: dto.HtmlText,
            author: dto.Author,
            credits: dto.Credits,
            isPublished: dto.IsPublished,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow
        );

        await context.Quotes.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<QuoteCollectionEntity> CreateCollectionAsync(
        CreateQuoteCollectionDto dto
    )
    {
        var entity = new QuoteCollectionEntity(
            id: dto.Id,
            exportId: dto.ExportId,
            name: dto.Name,
            description: dto.Description,
            isPublished: dto.IsPublished,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow
        );

        await context.QuoteCollections.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DoesCollectionExistAsync(Guid id)
        => await context.QuoteCollections.AnyAsync(qc => qc.Id == id);

    public async Task<Paginated<QuoteEntity>> GetQuotesAsync(
        PaginationQuery pagination,
        SearchQuery search,
        bool includeCollection = false
    )
    {
        var query = context.Quotes.AsNoTracking()
            .OrderByDescending(q => q.CreatedAt)
            .AsQueryable();

        if (includeCollection) query = query.Include(q => q.QuoteCollection);
        query = ApplyQuoteSearch(query, search);

        return await query.ToPaginatedAsync(pagination, search);
    }

    public async Task<List<QuoteEntity>> GetRandomQuotesAsync(int count) => await context.Quotes
        .AsNoTracking()
        .Where(item => item.IsPublished)
        .OrderBy(_ => Guid.NewGuid())
        .Take(count)
        .ToListAsync();


    public async Task<QuoteEntity?> UpdateQuoteAsync(
        Guid id,
        UpdateQuoteDto dto
    )
    {
        var entity = await context.Quotes.FindAsync(id);
        if (entity is null) return null;

        entity.QuoteCollectionId = dto.QuoteCollectionId;
        entity.Text = dto.Text;
        entity.HtmlText = dto.HtmlText;
        entity.Author = dto.Author;
        entity.Credits = dto.Credits;
        entity.IsPublished = dto.IsPublished;

        entity.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<QuoteCollectionEntity?> UpdateCollectionAsync(
        Guid id,
        UpdateQuoteCollectionDto dto
    )
    {
        var entity = await context.QuoteCollections.FindAsync(id);
        if (entity is null) return null;

        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.IsPublished = dto.IsPublished;

        entity.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return entity;
    }

    private static IQueryable<QuoteEntity> ApplyQuoteSearch(
        IQueryable<QuoteEntity> query,
        SearchQuery search
    )
    {
        if (string.IsNullOrWhiteSpace(search.Query)) return query;
        var qLower = search.Query.Trim().ToLowerInvariant();

        return query.Where(q =>
            EF.Functions.Like(q.Text.ToLower(), $"%{qLower}%") ||
            (q.Author != null && EF.Functions.Like(q.Author.ToLower(), $"%{qLower}%"))
        );
    }

    // TODO - User or remove
    private static IQueryable<QuoteCollectionEntity> ApplyCollectionEntitySearch(
        IQueryable<QuoteCollectionEntity> query,
        SearchQuery search
    )
    {
        if (string.IsNullOrWhiteSpace(search.Query)) return query;
        var qLower = search.Query.Trim().ToLowerInvariant();

        return query.Where(qc =>
            EF.Functions.Like(qc.Name.ToLower(), $"%{qLower}%")
        );
    }
}

// TODO - Move to PietrSpace.Common.X
public static class PaginationExtensions
{
    public static async Task<Paginated<T>> ToPaginatedAsync<T>(
        this IQueryable<T> query,
        PaginationQuery pagination,
        SearchQuery search
    )
    {
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pagination.Size);

        var items = await query
            .Skip(pagination.Skip)
            .Take(pagination.Size)
            .ToListAsync();

        return new Paginated<T>(
            TotalItems: totalItems,
            TotalPages: totalPages,
            SearchQuery: search,
            PaginationQuery: pagination,
            Items: items
        );
    }
}