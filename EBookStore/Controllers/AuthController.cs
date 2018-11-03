using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EBookStore.Dto;
using EBookStore.Model;
using EBookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EBookStore.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        public AuthController(UserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            var hasher = new PasswordHasher<User>();
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            var userFromDb = _userRepository.GetByUsername(user.Username);
            var isVerify=hasher.VerifyHashedPassword(null, userFromDb.Password, user.Password);
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
                    claims:claims,
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

        [HttpGet,Route("logged")]
        [Authorize]
        public IActionResult GetLogged()
        {
            var username = User.Identity.Name;
            var user = _userRepository.GetByUsername(username);
            if (user != null)
                return Ok(_mapper.Map<UserDto>(user));
            return NotFound();
        }
    }
}