using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NomsNoms.Interfaces;

namespace NomsNoms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AdminController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _userRepository.GetAllUserAdmin());
        }
    }
}
