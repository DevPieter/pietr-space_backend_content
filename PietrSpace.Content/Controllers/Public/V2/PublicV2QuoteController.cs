using Microsoft.AspNetCore.Mvc;
using PietrSpace.Content.Models.Responses.Quotes.Public;
using PietrSpace.Content.Quotes.Logic.Services;

namespace PietrSpace.Content.Controllers.Public.V2;

[ApiController, Route($"{Routes.Controller.Content.Version2.Public}/quotes")]
public class PublicV2QuoteController(QuoteService quoteService) : ControllerBase
{
    [HttpGet("random")]
    public async Task<IActionResult> GetRandomQuotes()
    {
        var result = await quoteService.GetRandomQuotesAsync();

        // TODO - Implement proper error handling
        if (result.IsError || result.Data is null) return NotFound("TODO: Implement proper error handling.");

        return Ok(result.Data.Select(q => new PublicQuoteResponse(
            q.Text,
            q.HtmlText,
            q.Author,
            q.Credits
        )).ToList());
    }
}