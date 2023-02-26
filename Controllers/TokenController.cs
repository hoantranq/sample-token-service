using Microsoft.AspNetCore.Mvc;
using Sample.API.Dtos;
using Sample.API.Services;

namespace Sample.API;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("jwe-token")]
    public async Task<ActionResult> GetJweToken([FromBody] JweRequest request)
    {
        var result = _tokenService.GetJweTokenAsync(request);

        return Ok(await Task.FromResult(""));
    }
}