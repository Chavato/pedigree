using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pedigree.Application.Interfaces;
using Pedigree.Application.Models.DTOs;
using Pedigree.Domain.Exceptions;
using Pedigree.WebApi.Models;

namespace Pedigree.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : DefaultController
    {

        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(RegisterUserDTO userRegister)
        {
            try
            {
                ValidateModelState();

                await _userService.RegisterUserAsync(userRegister);

                return Ok(new { Message = "User registered" });
            }

            catch (BadHttpRequestException err)
            {
                return Problem(detail: err.Message, statusCode: 400);
            }

            catch (ArgumentException err)
            {
                return Problem(detail: err.Message, statusCode: 400);
            }

            catch (Exception err)
            {
                if (err.InnerException != null)
                {
                    return Problem(detail: err.InnerException.Message, statusCode: 500);
                }
                return Problem(detail: err.Message, statusCode: 500);
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginUserDTO user)
        {
            try
            {
                ValidateModelState();

                if (await _userService.AuthenicateUserAsync(user))
                {
                    LoginResponse loginResponse = new LoginResponse
                    {
                        Token = await GenerateTokenAsync(user.Email)
                    };

                    return Ok(loginResponse);
                }
                else
                {
                    return Problem(detail: "User or/and Password are incorrect.", statusCode: 403);
                }
            }

            catch (EntityNotFoundException)
            {
                return Problem(detail: "User or/and Password are incorrect.", statusCode: 403);
            }

            catch (BadHttpRequestException err)
            {
                return Problem(detail: err.Message, statusCode: 400);
            }

            catch (Exception err)
            {
                if (err.InnerException != null)
                {
                    return Problem(detail: err.InnerException.Message, statusCode: 500);
                }
                return Problem(detail: err.Message, statusCode: 500);
            }
        }

        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordModel)
        {
            try
            {
                ValidateModelState();

                string? userName = HttpContext.User.Identity!.Name;

                if (userName == null)
                    throw new Exception("Problem with access user name.");

                await _userService.ChangePasswordAsync(changePasswordModel, userName);

                return NoContent();
            }

            catch (EntityNotFoundException err)
            {
                return Problem(detail: err.Message, statusCode: 404);
            }

            catch (ArgumentException err)
            {
                return Problem(detail: err.Message, statusCode: 400);
            }

            catch (Exception err)
            {
                if (err.InnerException != null)
                {
                    return Problem(detail: err.InnerException.Message, statusCode: 500);
                }
                return Problem(detail: err.Message, statusCode: 500);
            }
        }

        [HttpDelete("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.LogoutAsync();

                return NoContent();
            }
            catch (Exception err)
            {
                if (err.InnerException != null)
                {
                    return Problem(detail: err.InnerException.Message, statusCode: 500);
                }
                return Problem(detail: err.Message, statusCode: 500);
            }
        }


        [HttpDelete("[action]")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete()
        {
            try
            {
                string? email = HttpContext.User.Identity!.Name;

                if (email == null)
                    throw new Exception("Problem with access user name.");

                await _userService.DeleteUserByEmailAsync(email);

                return NoContent();
            }

            catch (EntityNotFoundException err)
            {
                return Problem(detail: err.Message, statusCode: 404);
            }

            catch (Exception err)
            {
                if (err.InnerException != null)
                {
                    return Problem(detail: err.InnerException.Message, statusCode: 500);
                }
                return Problem(detail: err.Message, statusCode: 500);
            }
        }


        private async Task<string> GenerateTokenAsync(string userName)
        {
            var user = await _userService.GetUserByEmailAsync(userName);

            var claims = new List<Claim>
                {
                new Claim("name", userName),
                new Claim(ClaimTypes.Name, userName),
                new Claim("id", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            string? jwtSecret = _configuration.GetValue<string>("JWT_SECRET");
            string? jwtValidIssuer = _configuration.GetValue<string>("JWT_VALID_ISSUER");
            string? jwtValidAudience = _configuration.GetValue<string>("JWT_VALID_AUDIENCE");

            if (jwtSecret == null || jwtValidIssuer == null || jwtValidAudience == null)
            {
                throw new InvalidOperationException("JWT_SECET, JWT_VALID_ISSUER or JWT_VALID_AUDIENCE are missing");
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var token = new JwtSecurityToken(
                issuer: jwtValidIssuer,
                audience: jwtValidAudience,
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token).ToString();

            return tokenString;
        }

    }
}