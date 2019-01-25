using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UES.EbookRepository.BLL.Contract.Contracts;
using UES.EbookRepository.BLL.Contract.DTOs;

namespace UES.EbookRepository.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager _userManager;

        public AuthController(IUserManager userManager)
        {
            _userManager = userManager;

        }


        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            var hasher = new PasswordHasher<LoginModel>();
            var userFromDb = _userManager.GetByUsername(user.Username);
            if (userFromDb == null)
            {
                return BadRequest("Invalid client request");
            }

            var isVerify = hasher.VerifyHashedPassword(null, userFromDb.Password, user.Password);
            if (userFromDb != null && isVerify == PasswordVerificationResult.Success)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@3"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
                var claims = new List<Claim>();
                if (userFromDb.Type == "Admin")
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                else
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim(ClaimTypes.Name, userFromDb.Username));
                var tokeOptions = new JwtSecurityToken(
                    issuer: "mysite.com",
                    audience: "mysite.com",
                    claims: claims,
                    expires: DateTime.Now.AddHours(5),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet, Route("logged")]
        [Authorize]
        public IActionResult GetLogged()
        {
            var username = User.Identity.Name;
            var user = _userManager.GetByUsername(username);
            if (user != null)
                return Ok(user);
            return NotFound();
        }
    }
}