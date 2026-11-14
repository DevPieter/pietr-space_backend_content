using Microsoft.AspNetCore.Mvc;
using PietrSpace.Content.Info.Logic.Services;

namespace PietrSpace.Content.Controllers.Public.V2;

[ApiController, Route($"{Routes.Controller.Content.Version2.Public}/info")]
public class PublicV2InfoController(InfoService infoService) : ControllerBase
{
    [HttpGet("{key}")]
    public async Task<IActionResult> GetInfoEntry([FromRoute] string key)
    {
        var result = await infoService.GetInfoEntryAsync(key);
        if (result.Data is null) return NotFound(result.Message);

        return Ok(result.Data.Value);
    }
}
