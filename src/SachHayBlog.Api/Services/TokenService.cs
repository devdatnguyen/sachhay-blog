using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SachHayBlog.Core.ConfigOptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SachHayBlog.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtTokenSettings _jwtTokenSettings;
        public TokenService(IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            _jwtTokenSettings = jwtTokenSettings.Value;
        }
        public string GenarateAccessToken(IEnumerable<Claim> claims)
        {
            var secrectKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.Key));
            var signingCredentials = new SigningCredentials(secrectKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                    issuer: _jwtTokenSettings.Issuer,
                    audience: _jwtTokenSettings.Issuer,
                    claims: claims,
                    expires: DateTime.Now.AddHours(_jwtTokenSettings.ExpireInHours),
                    signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public string GenarateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        /// <summary>
        /// Get user principal from expired token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="SecurityTokenException"></exception>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
