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
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                    };

                    var token = await GetToken(authClaims);


                    return Ok(new
                    {
                        token = token,
                        id = user.Id
                    });
                }
            }
            return BadRequest(ModelState);

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
                        Email = registerViewModel.Email
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

        private async Task<string> GetToken(List<Claim> authClaims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is a 126 bit string sentence."));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: credentials);
            try
            {
                var token = new JwtSecurityTokenHandler().WriteToken(secToken);
                return token;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
