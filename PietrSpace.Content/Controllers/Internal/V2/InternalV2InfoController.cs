using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PietrSpace.Content.Info.Logic.Services;

namespace PietrSpace.Content.Controllers.Internal.V2;

[ApiController, Route($"{Routes.Controller.Content.Version2.Internal}/info")]
public class InternalV2InfoController(InfoService infoService) : ControllerBase
{
    [HttpPut("{key}")]
    public async Task<IActionResult> UpsertInfoEntry([FromRoute] string key, [FromBody] JsonElement value)
    {
        var result = await infoService.UpsertInfoEntryAsync(key, value);
        if (result.IsError) return BadRequest(result.Message);

        return NoContent();
    }
}
