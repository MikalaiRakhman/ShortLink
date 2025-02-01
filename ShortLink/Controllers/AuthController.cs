using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShortLink.DAL.Data;
using ShortLink.DAL.Identity;
using ShortLink.DAL.Identity.Enums;
using ShortLink.Domain.Entities;
using ShortLink.Web.Models;

namespace ShortLink.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenProvider _tokenProvider;
        private readonly ApplicationDbContext _context;

        public AuthController(UserManager<User> userManager, TokenProvider tokenProvider, ApplicationDbContext context)
        {
            _tokenProvider = tokenProvider;
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <responce code="200">User registered successfully.</responce>
        /// <response code="400">Error messege.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var appUser = new User
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(appUser, Role.User.ToString());            
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully!" });
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <response code="400">Error messege.</response>
        /// <responce code="200">Acces-token and refresh-token.</responce>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByEmailAsync(model.Email);

            Guard.AgainstUnauthorized(applicationUser);

            var isValidPassword = await _userManager.CheckPasswordAsync(applicationUser, model.Password);

            Guard.AgainsInvalidPassword(isValidPassword);

            var roles = await _userManager.GetRolesAsync(applicationUser);
            var token = _tokenProvider.GenerateJwtToken(applicationUser, roles);
            var refreshToken = _tokenProvider.GenerateRefreshToken(applicationUser.Id, cancellationToken);

            return Ok(new { Token = token, RefreshToken = refreshToken.Result });
        }

        /// <summary>
        /// New refresh-token and acces-token.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="401">Error messege.</response>
        /// <responce code="200">New acces-token and refresh-token.</responce>
        /// <returns>New refresh-token and acces-token.</returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model, CancellationToken cancellationToken)
        {
            var (newJwtToken, newRefreshToken) = await _tokenProvider.RefreshTokens(model.RefreshToken, cancellationToken);

            return Ok(new { Token = newJwtToken, RefreshToken = newRefreshToken });
        }        
    }
}
