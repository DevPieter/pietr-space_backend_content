using Microsoft.AspNetCore.Mvc;
using PietrSpace.Common.Requests.Queries;
using PietrSpace.Content.Models.Requests.Quotes;
using PietrSpace.Content.Models.Responses.Quotes.Internal;
using PietrSpace.Content.Quotes.Logic.Models.Args.Quotes;
using PietrSpace.Content.Quotes.Logic.Services;

namespace PietrSpace.Content.Controllers.Internal.V2;

[ApiController, Route($"{Routes.Controller.Content.Version2.Internal}/quotes")]
public class InternalV2QuoteController(QuoteService quoteService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetQuotesPaginated(
        [FromQuery] PaginationQuery pagination,
        [FromQuery] SearchQuery search
    )
    {
        var result = await quoteService.GetQuotesAsync(pagination, search);

        // TODO - Implement proper error handling
        if (result.IsError || result.Data is null) return NotFound("TODO: Implement error handling.");
        return Ok(new PaginatedQuotesResponse(result.Data, pagination, search));
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuote(
        [FromBody] CreateQuoteRequest request
    )
    {
        var result = await quoteService.CreateQuoteAsync(new CreateQuoteArg(
            QuoteCollectionId: request.QuoteCollectionId,
            Text: request.Text,
            HtmlText: request.HtmlText,
            Author: request.Author,
            Credits: request.Credits,
            IsPublished: request.IsPublished
        ));

        // TODO - Implement proper error handling
        if (result.IsError || result.Data is null) return NotFound("TODO: Implement error handling.");

        return Ok(result.Data);
    }
}