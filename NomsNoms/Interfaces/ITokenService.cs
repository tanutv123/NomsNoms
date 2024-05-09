using NomsNoms.DTOs;

namespace NomsNoms.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateTokenString(LoginDTO loginDTO);
    }
}