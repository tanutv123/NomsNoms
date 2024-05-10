using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Interfaces;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AdminController(IUserRepository userRepository, IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("users")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _userRepository.GetAllUserAdmin());
        }
        [HttpPost("users/create-user")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateAdminDTO userDTO)
        {
            try
            {
                var user = new AppUser
                {
                    Email = userDTO.Email,
                    PhoneNumber = userDTO.PhoneNumber,
                    KnownAs = userDTO.Name,
                    UserName = userDTO.Email,
                    Introduction = userDTO.Introduction,
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    LastActive = DateTime.Now
                };
                await _userRepository.CreateUserAdmin(user);

                var userRole = new AppUserRole
                {
                    UserId = user.Id,
                    RoleId = userDTO.Role
                };

                user.UserRoles = new List<AppUserRole> { userRole };

                user.UserPhoto = new UserPhoto
                {
                    AppUserId = user.Id,
                    Url = "https://randomuser.me/api/portraits/men/80.jpg"
                };

                await _userRepository.UpdateUserAdmin(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("users/update-user")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateUserAdmin([FromBody] UserAdminDTO userDto)
        {
            try
            {
                var user = _mapper.Map<AppUser>(userDto);
                await _userRepository.UpdateUserAdmin(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated Successfully");
        }

        [HttpDelete("users/delete-user")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteUserAdmin([FromBody] UserAdminDTO userDto)
        {
            var user = _mapper.Map<AppUser>(userDto);
            await _userRepository.DeleteUserAdmin(user);
            return Ok("Deleted Successfully");
        }
    }
}
