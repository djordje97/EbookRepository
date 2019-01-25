using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UES.EbookRepository.BLL.Contract.Contracts;
using UES.EbookRepository.BLL.Contract.DTOs;

namespace UES.EbookRepository.Presentation.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsers()
        {
            var users = _userManager.GetAll();
           
            return Ok(users);
        }

        [HttpGet("{username}")]
        [AllowAnonymous]
        public IActionResult GetUser(string username)
        {
            var user = _userManager.GetByUsername(username);
            if (user == null)
                return NotFound();
            user.Password = "";
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddUser([FromBody]UserDTO userDto)
        {
            if (userDto == null)
                return BadRequest();
            var hasher = new PasswordHasher<UserDTO>();
            userDto.Password = hasher.HashPassword(null,userDto.Password);
            userDto.Type = "User";
            var user = _userManager.Create(userDto);

            return Created(new Uri("http://localhost:12621/api/user/" + user.UserId), user);
        }

        [HttpPut("{username}")]
        [Authorize]
        public IActionResult UpdateUser(string username, [FromBody] UserDTO userDto)
        {
            var hasher = new PasswordHasher<UserDTO>();
            if (userDto == null)
                return BadRequest();
            var userFromDb = _userManager.GetByUsername(username);
            if (userFromDb == null)
                return BadRequest();
            if (userDto.Password == "")
                userDto.Password = userFromDb.Password;
            else
            {
                userDto.Password = hasher.HashPassword(null, userDto.Password);
            }
            
            userFromDb = _userManager.Update(userDto);
            
            return Ok(userFromDb);
        }

        [HttpDelete("{username}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(string username)
        {
            var userFromDb = _userManager.GetByUsername(username);
            if (userFromDb == null)
                return BadRequest();
            _userManager.Delete(userFromDb.UserId);
            return Ok();

        }
    }
}