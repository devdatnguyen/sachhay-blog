using System.Security.Claims;

namespace SachHayBlog.Api.Services
{
    public interface ITokenService
    {
        string GenarateAccessToken(IEnumerable<Claim> claims);
        string GenarateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
