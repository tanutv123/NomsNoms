using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NomsNoms.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AuthRepository(UserManager<AppUser> userManager, IConfiguration configuration, IMapper mapper, IUserRepository userRepository)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> Register(RegisterDTO registerBody)
        {
            var user = new AppUser
            {
                UserName = registerBody.Email,
                Email = registerBody.Email,
                KnownAs = registerBody.Name,
                PhoneNumber = registerBody.PhoneNumber,
                Introduction = "",
            };
            if (await IsPhoneExistAsync(user.PhoneNumber))
            {
                throw new Exception("Phone is exist");
            }
            var result = await _userManager.CreateAsync(user, registerBody.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Client");
            }
            return result;
        }

        public async Task<bool> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(user, loginDTO.Password);
        }

        public async Task<bool> LoginSuperAdmin(string email, string password)
        {
            var superAdminEmail = _configuration["SuperAdminCredential:Email"];
            var superAdminPassword = _configuration["SuperAdminCredential:Password"];

            return email == superAdminEmail && password == superAdminPassword;
        }

        public async Task<UserDTO> GetUserDTO(string email, string tokenString)
        {
            var user = await _userRepository.GetAllUserToken().Where(x => x.Email == email).ProjectTo<UserDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            if (user != null && !string.IsNullOrEmpty(tokenString))
            {
                var result = _mapper.Map<UserDTO>(user);
                result.Token = tokenString;
                return result;
            }
            return null;
        }
        public async Task<bool> IsPhoneExistAsync(string phone)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
            return user != null;
        }
    }
}
