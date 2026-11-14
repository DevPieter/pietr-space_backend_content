using PietrSpace.Common.Requests.Queries;
using PietrSpace.Common.Responses;
using PietrSpace.Common.Results;
using PietrSpace.Content.Quotes.Data.Models.Entities;
using PietrSpace.Content.Quotes.Data.Repositories;
using PietrSpace.Content.Quotes.Logic.Models;
using PietrSpace.Content.Quotes.Logic.Models.Args.Quotes;

namespace PietrSpace.Content.Quotes.Logic.Services;

public class QuoteService(IQuoteRepository repository)
{
    public async Task<ResultObject<Quote>> CreateQuoteAsync(
        CreateQuoteArg arg
    )
    {
        var validationResult = Quote.ValidateCreate(arg);
        if (validationResult.IsError) return validationResult.CloneWithoutData<Quote>();

        if (arg.QuoteCollectionId is not null)
        {
            var exists = await repository.DoesCollectionExistAsync(arg.QuoteCollectionId.Value);
            if (!exists) return ResultObject<Quote>.Validation("Quote collection does not exist.");
        }

        try
        {
            var domain = new Quote(arg);
            await repository.CreateQuoteAsync(domain.ToCreateQuoteDto());
            return ResultObject<Quote>.Success(domain);
        }
        catch (Exception ex)
        {
            return ResultObject<Quote>.Failure("Failed to create quote.");
        }
    }

    public async Task<ResultObject<Paginated<Quote>>> GetQuotesAsync(
        PaginationQuery pagination,
        SearchQuery search,
        bool includeCollection = false
    )
    {
        var paginationValidationResult = pagination.Validate();
        if (paginationValidationResult.IsError) return paginationValidationResult.CloneWithoutData<Paginated<Quote>>();

        var searchValidationResult = pagination.Validate();
        if (searchValidationResult.IsError) return searchValidationResult.CloneWithoutData<Paginated<Quote>>();

        try
        {
            var entities = await repository.GetQuotesAsync(
                pagination: pagination,
                search: search,
                includeCollection: includeCollection
            );

            var domain = entities.Map<QuoteEntity, Quote>(entity => new Quote(entity));
            return ResultObject<Paginated<Quote>>.Success(domain);
        }
        catch (Exception ex)
        {
            return ResultObject<Paginated<Quote>>.Failure("Failed to create quote.");
        }
    }

    public async Task<ResultObject<List<Quote>>> GetRandomQuotesAsync()
    {
        try
        {
            var entities = await repository.GetRandomQuotesAsync(8);
            var domain = entities.Select(entity => new Quote(entity)).ToList();
            return ResultObject<List<Quote>>.Success(domain);
        }
        catch (Exception ex)
        {
            return ResultObject<List<Quote>>.Failure("Failed to get random quotes.");
        }
    }
}

// TODO - Move to model
public static class TempExtensions
{
    public static ResultObject<TR> CloneWithoutData<TR>(this Result result)
        => new(result.Type, default, result.Message);

    public static Paginated<TC> Map<T, TC>(this Paginated<T> paginated, Func<T, TC> mapFunc) => new(
        TotalItems: paginated.TotalItems,
        TotalPages: paginated.TotalPages,
        SearchQuery: paginated.SearchQuery,
        PaginationQuery: paginated.PaginationQuery,
        Items: paginated.Items.Select(mapFunc).ToList() ?? []
    );

    public static Result Validate(this PaginationQuery query)
    {
        if (query.Page < 1) return Result.Validation("Page must be greater than or equal to 1.");
        if (query.Size < 1) return Result.Validation("Size must be greater than or equal to 1.");
        if (query.Size > 100) return Result.Validation("Size must be less than or equal to 100.");

        return Result.Success();
    }

    public static Result Validate(this SearchQuery query)
    {
        if (query.Query?.Length > 255) return Result.Validation("Query too long");
        return Result.Success();
    }
}