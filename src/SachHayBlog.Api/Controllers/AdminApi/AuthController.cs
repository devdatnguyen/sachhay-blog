using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SachHayBlog.Api.Services;
using SachHayBlog.Core.Domain.Identity;
using SachHayBlog.Core.Models.Auth;
using SachHayBlog.Core.SeedWorks.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SachHayBlog.Api.Controllers.AdminApi
{
    [Route("api/admin/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthController(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            ITokenService tokenService 
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticatedResult>> Login([FromBody] LoginRequest request)
        {
            //Authentication
            if(request == null)
            {
                return BadRequest("Invalid login request");
            }

            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user == null || !user.IsActive || user.LockoutEnabled)
            {
                return Unauthorized("Invalid user");
            }

            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);
            if(!result.Succeeded)
            {
                return Unauthorized("Invalid password");
            }

            //Authorization
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(UserClaims.Id, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(UserClaims.FirstName, user.FirstName),
                new Claim(UserClaims.Roles, string.Join(";", roles)),
                //new Claim(UserClaims.Permission, JsonSerializer.Serialize(permissions)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var accessToken = _tokenService.GenarateAccessToken(claims);
            var refreshToken = _tokenService.GenarateRefreshToken() ;

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(30);
            await _userManager.UpdateAsync(user);

            return Ok(new AuthenticatedResult
            {
                Token = accessToken,
                RefreshToken = refreshToken
            }) ;
        }
    }
}
