using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Helpers;
using NomsNoms.Interfaces;

namespace NomsNoms.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository authRepository, ITokenService tokenService, IMapper mapper, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _mapper = mapper;
            _userRepository = userRepository;   
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                var result = await _authRepository.Register(registerDTO);
                if (result.Succeeded)
                {
                    var loginDTO = new LoginDTO
                    {
                        Email = registerDTO.Email,
                        Password = registerDTO.Password
                    };
                    var tokenString = await _tokenService.GenerateTokenString(loginDTO);
                    var userDTO = await _authRepository.GetUserDTO(loginDTO.Email, tokenString);
                    return Ok(userDTO);
                }
                return BadRequest(result.Errors);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(await _authRepository.Login(loginDTO))
            {
                var tokenString = await _tokenService.GenerateTokenString(loginDTO);
                var userDTO = await _authRepository.GetUserDTO(loginDTO.Email, tokenString);
                return Ok(userDTO);
            }
            if(await _authRepository.LoginSuperAdmin(loginDTO.Email, loginDTO.Password))
            {
                return Ok();
            }
            return BadRequest("Invalid username or password");
        }
    }
}
