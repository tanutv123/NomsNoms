using Microsoft.AspNetCore.Identity;
using NomsNoms.DTOs;

namespace NomsNoms.Interfaces
{
    public interface IAuthRepository
    {
        Task<UserDTO> GetUserDTO(string email, string tokenString);
        Task<bool> IsPhoneExistAsync(string phone);
        Task<bool> Login(LoginDTO loginDTO);
        Task<IdentityResult> Register(RegisterDTO registerBody);
        Task<bool> LoginSuperAdmin(string email, string password);
    }
}