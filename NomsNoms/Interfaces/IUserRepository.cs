using NomsNoms.DTOs;
using NomsNoms.Entities;

namespace NomsNoms.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<AppUser> GetAllUserToken();
        Task<List<UserAdminDTO>> GetAllUserAdmin();
        Task UpdateUserDetail(AppUser user);
    }
}