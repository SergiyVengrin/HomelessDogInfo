using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using CommentsWebApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CommentsWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        private string generatedToken = null;

        public UserController(IConfiguration configuration, IUserService userService, ITokenService tokenService, IMapper mapper)
        {
            _configuration = configuration;
            _userService = userService;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        private void GenerateToken(UserDTO userDto)
        {
            if (generatedToken == null)
            {
                var mappedUser = _mapper.Map<UserModel>(userDto);
                System.Diagnostics.Debug.WriteLine(mappedUser.AccessLevel);

                generatedToken = _tokenService.BuildToken(
                    _configuration["Jwt:Key"].ToString(),
                    _configuration["Jwt:Issuer"].ToString(),
                    _mapper.Map<UserModel>(userDto));

                if (generatedToken != null)
                {
                    HttpContext.Session.SetString("Token", generatedToken);
                }
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            await _userService.Register(_mapper.Map<UserModel>(userDto));
            GenerateToken(userDto);
            return Ok(new { token = generatedToken });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserDTO userDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _userService.Login(_mapper.Map<UserModel>(userDto)))
                    {
                        GenerateToken(userDto);
                        return Ok(new { token = generatedToken });
                    }
                }
                catch
                {
                    return StatusCode(500, "Internal error");
                }
            }

            return BadRequest();
        }
    }
}
