using NomsNoms.DTOs;
using NomsNoms.Entities;

namespace NomsNoms.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<AppUser> GetAllUserToken();
        Task<List<UserAdminDTO>> GetAllUserAdmin();
        Task UpdateUserDetail(AppUser user);
        Task CreateUserAdmin(AppUser user);
        Task DeleteUserAdmin(AppUser user);
        Task UpdateUserAdmin(AppUser user);
        Task Follow(string email, int userid);
        Task<List<UserFollowToShowDTO>> GetTopCookByFollower();
        Task<AppUser> GetUserById(int id);
        Task<List<AppUser>> GetFollowerByCookId(int userId);
        Task<bool> BuySubscription(string cookEmail,string email, int subscriptionId);
        Task<bool> HasUserHasAlreadySub(string cookEmail, string email);
        Task RecipeView(int recipeId);
        Task<TasteProfile> GetUserTasteProfile(string email);
    }
}