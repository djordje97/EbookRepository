﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EBookStore.Dto;
using EBookStore.Model;
using EBookStore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EBookStore.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserRepository _userRepository;
        IMapper _mapper;

        public UserController(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetAll();
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                userDtos.Add(_mapper.Map<UserDto>(user));

            }
            return Ok(userDtos);
        }

        [HttpGet("{username}")]
        public IActionResult GetUser(string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null)
                return NotFound();
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        public IActionResult AddUser([FromBody]UserDto userDto)
        {
            if (userDto == null)
                return BadRequest();
            var user = _mapper.Map<User>(userDto);
            user = _userRepository.Save(user);
            _userRepository.Complete();

            return Created(new Uri("http://localhost:12621/api/user/" + user.UserId), _mapper.Map<UserDto>(user));
        }

        [HttpPut("{username}")]
        public IActionResult UpdateUser(string username, [FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest();
            var userFromDb = _userRepository.GetByUsername(username);
            if (userFromDb == null)
                return BadRequest();
            userFromDb.Firstname = userDto.Firstname;
            userFromDb.Lastname = userFromDb.Lastname;
            userFromDb.Type = userDto.Type;
            userFromDb = _userRepository.Update(userFromDb);
            _userRepository.Complete();
            return Ok(_mapper.Map<UserDto>(userFromDb));
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            var userFromDb = _userRepository.GetByUsername(username);
            if (userFromDb == null)
                return BadRequest();
            _userRepository.Delete(userFromDb);
            _userRepository.Complete();
            return Ok();

        }
    }
}