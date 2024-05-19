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
        Task<UserProfileDTO> GetUserProfile(string email);
        Task Follow(string email, int userid);
        Task<List<UserFollowToShowDTO>> GetTopCookByFollower();
        Task<AppUser> GetUserById(int id);
        Task<List<UserProfileDTO>> GetFollowerByCookId(string email);
        Task<bool> BuySubscription(int userId, int subscriptionId);
        Task<bool> HasUserHasAlreadySub(string cookEmail, string email);
        Task<bool> HasLiked(string cookEmail, int id);
        Task RecipeView(int recipeId);
        Task<TasteProfile> GetUserTasteProfile(string email);
        Task EnableUserAdmin(string email);
        Task UpdateUserPhoto(UserPhotoDTO userPhotoDTO);
        Task<UserPhoto> GetUserPhotoByUserEmail(string email);
        Task AddTransaction(TransactionDTO transactionDTO);
        Task<List<Transaction>> GetAllUserTransaction();
        Task<List<Transaction>> GetUserTransaction(string email);
        Task UpdateUserLastActive(int id);
        Task<List<UserProfileDTO>> GetSubberByCookId(string email);
        Task FollowUser(string cookEmail, string userEmail);
        Task<SubscriptionUserDTO> GetUserSubscription(string email);
        Task<Subscription> GetSubscriptionById(int id);
        Task UpdateUserSubscription(UpdateUserSubsciprtionDTO updateDto);
        Task AddPaymentIntent(long orderCode, int userId, int subscriptionId);
        Task<SubscriptionPayment> GetPaymentIntent(long orderCode);
        Task SetTasteProfileUser(TasteProfileDTO tasteProfileDTO, string userEmail);
    }
}