using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskMan.BusinessLogic;
using TaskMan.BusinessObjects;
using TaskMan.ViewModels;

namespace TaskMan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<ApplicationUser> user, IConfiguration configuration)
        {
            userManager = user;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginViewModel.Email);
                if (user != null && await userManager.CheckPasswordAsync(user, loginViewModel.Password))
                {

                    var token = await GetToken();


                    return Ok(token);
                }
            }
            return Unauthorized();

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> PostRegister([FromBody] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(registerViewModel.Email);
                if (user != null)
                {
                    return BadRequest();
                }
                else
                {
                    var userDo = new ApplicationUser()
                    {
                        FirstName = registerViewModel.FirstName,
                        LastName = registerViewModel.LastName,
                        Email = registerViewModel.Email,
                        UserName = registerViewModel.Email.Split("@")[0]
                    };
                    var result = await userManager.CreateAsync(userDo, registerViewModel.Password);
                    if (result.Succeeded)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("Error while creating user");
                    }
                }

            }
            return BadRequest(ModelState);

        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] TokenRequestViewModel request)
        {
            var principal = GetPrincipal(request.Token);
            if (principal == null)
            {
                return Unauthorized();
            }

            var tokens = GetToken();
            return Ok(tokens);
        }

        private async Task<TokenResponseViewModel> GetToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is a 126 bit string sentence."));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            var refreshToken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials);
            try
            {
                var tokenResponse = new TokenResponseViewModel();
                tokenResponse.AccessToken = new JwtSecurityTokenHandler().WriteToken(secToken);
                tokenResponse.RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);

                return tokenResponse;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private ClaimsPrincipal GetPrincipal(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

    }
}
