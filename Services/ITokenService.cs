using Sample.API.Dtos;

namespace Sample.API.Services;

public interface ITokenService
{
    Task<string> GetJweTokenAsync(JweRequest request);
}