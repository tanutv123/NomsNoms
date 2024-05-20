using AutoMapper;
using AutoMapper.QueryableExtensions;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Interfaces;

namespace NomsNoms.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(DataContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IQueryable<AppUser> GetAllUserToken()
        {
            return _context.Users.Include(u => u.UserRoles).ThenInclude(u => u.AppRole).Include(u => u.UserPhoto);
        }

        public async Task<List<UserAdminDTO>> GetAllUserAdmin()
        {
            List<UserAdminDTO> users = null;
            try
            {
                var list = await _context.Users.Include(u => u.UserRoles).ThenInclude(u => u.AppRole).Include(u => u.UserPhoto).ToListAsync();
                users = _mapper.Map<List<UserAdminDTO>>(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }
        public async Task<bool> IsPhoneExistAsync(string phone)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phone);
            return user != null;
        }
        public async Task UpdateUserDetail(AppUser user)
        {
            try
            {
                var u = await _userManager.FindByEmailAsync(user.Email);
                if (u != null)
                {
                    if (u.PhoneNumber != user.PhoneNumber && await IsPhoneExistAsync(user.PhoneNumber))
                    {
                        throw new Exception("Phone is exist");
                    }
                    u.KnownAs = user.KnownAs;
                    u.PhoneNumber = user.PhoneNumber;
                    u.Introduction = user.Introduction;
                    await _userManager.UpdateAsync(u);
                }
                else
                {
                    throw new Exception("User is not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CreateUserAdmin(AppUser user)
        {
            try
            {
                var u = await _userManager.FindByEmailAsync(user.Email);
                if (u == null)
                {
                    string password = "Pa$$w0rd";
                    if (await IsPhoneExistAsync(user.PhoneNumber))
                    {
                        throw new Exception("Phone is exist");
                    }
                    var result = await _userManager.CreateAsync(user, password);
                    if (!result.Succeeded)
                    {
                        throw new Exception("User creation failed");
                    }
                }
                else
                {
                    throw new Exception("User is exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateUserAdmin(AppUser user)
        {
            try
            {
                var u = await _userManager.FindByEmailAsync(user.Email);
                if (u != null)
                {
                    if (u.PhoneNumber != user.PhoneNumber && await IsPhoneExistAsync(user.PhoneNumber))
                    {
                        throw new Exception("Phone is exist");
                    }
                    u.KnownAs = user.KnownAs;
                    u.Email = user.Email;
                    u.PhoneNumber = user.PhoneNumber;
                    u.UserRoles = user.UserRoles;
                    u.UserPhoto = user.UserPhoto;
                    u.Introduction = user.Introduction;
                    u.Status = user.Status;
                    u.SubscriptionId = user.SubscriptionId;
                    u.TasteProfileId = user.TasteProfileId;
                    u.CreatedDate = DateTime.Now;
                    u.LastActive = DateTime.Now;
                    await _userManager.UpdateAsync(u);
                }
                else
                {
                    throw new Exception("User is not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteUserAdmin(AppUser user)
        {
            try
            {
                var u = await _userManager.FindByEmailAsync(user.Email);
                if (u != null)
                {
                    u.Status = 0;
                    await _userManager.UpdateAsync(u);
                }
                else
                {
                    throw new Exception("User is not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task Follow(string email, int userid)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var cookId = user.Id;
                var isFollow = await _context.UserFollows.AnyAsync(a => a.TargetUserId == cookId && a.SourceUserId == userid);
                if (cookId != null && userid != null && isFollow)
                {
                    var index = await _context.UserFollows.Where(a => a.TargetUserId == cookId && a.SourceUserId == userid).FirstOrDefaultAsync();
                    _context.UserFollows.Remove(index);
                    await _context.SaveChangesAsync();
                }
                else if (cookId != null && userid != null)
                {
                    var follow = new UserFollow
                    {
                        TargetUserId = cookId,
                        SourceUserId = userid
                    };
                    await _context.UserFollows.AddAsync(follow);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<AppUser> GetUserById(int id)
        {
            AppUser users = null;
            try
            {
                var list = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return users;
        }

        public async Task<List<UserFollowToShowDTO>> GetCountFollower()
        {
            try
            {
                var users = await _context.Users.Include(u => u.UserPhoto).ToListAsync();
                int count = users.Count();
                List<UserFollowToShowDTO> followers = new List<UserFollowToShowDTO>();
                for (int i = 0; i < count; i++)
                {
                    var user = users[i];
                    var countFollower = _context.UserFollows.Where(u => u.SourceUserId == user.Id).Count();
                    UserFollowToShowDTO follower = new UserFollowToShowDTO
                    {
                        Email = user.Email,
                        FollowerCount = countFollower,
                        Name = user.UserName,
                        KnownAs = user.KnownAs,
                        ImageUrl = user.UserPhoto == null ? null : user.UserPhoto.Url
                    };
                    followers.Add(follower);
                }
                return followers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public async Task<List<UserFollowToShowDTO>> GetTopCookByFollower()
        {
            try
            {
                var list = await GetCountFollower();
                var orderList = list.OrderByDescending(f => f.FollowerCount).Take(10).ToList();

                return orderList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UserProfileDTO>> GetFollowerByCookId(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                List<UserProfileDTO> list = new List<UserProfileDTO>();
                var follow = await _context.UserFollows.Where(u => u.SourceUserId != user.Id).ToListAsync();
                foreach (var follower in follow)
                {
                    list.Add(
                        await _context.Users
                        .Where(x => x.Id == follower.SourceUserId)
                        .ProjectTo<UserProfileDTO>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync()
                        );
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> BuySubscription(int userId, int subscriptionId)
        {
            try
            {
                var existedSubscription = await _context.UserSubscriptions
                    .Where(x => x.AppUserId == userId && x.SubscriptionId == subscriptionId)
                    .ToListAsync();
                if (existedSubscription != null)
                {
                    foreach (var item in existedSubscription)
                    {
                        _context.UserSubscriptions.Remove(item);
                    }
                }
                var userSubscription = new UserSubscription
                {
                    AppUserId = userId,
                    SubscriptionId = subscriptionId,
                    StartedDate = DateTime.UtcNow,
                };
                await _context.UserSubscriptions.AddAsync(userSubscription);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> HasUserHasAlreadySub(string cookEmail, string email)
        {
            try
            {
                var sourceUser = await _userManager.FindByEmailAsync(email);
                var targetUser = await _userManager.FindByEmailAsync(cookEmail);
                var isSubed = await _context.AppUserSubscriptionRecords.AnyAsync(u => u.SourceUserId == sourceUser.Id && u.TargetUserId == targetUser.Id);

                return isSubed;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task RecipeView(int recipeId)
        {
            try
            {
                var recipeView = await _context.Recipes.Where(re => re.Id == recipeId).FirstOrDefaultAsync();
                recipeView.NumberOfViews += 1;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TasteProfile> GetUserTasteProfile(string email)
        {
            // Get the user from the database along with the TasteProfile
            var user = await _userManager.Users
                .SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                // Handle the case where the user was not found
                return null;
            }

            return await _context.TasteProfiles.FirstOrDefaultAsync(x => x.Id == user.TasteProfileId);
        }


        public async Task EnableUserAdmin(string email)
        {
            try
            {
                var u = await _userManager.FindByEmailAsync(email);
                if (u != null)
                {
                    u.Status = 1;
                    await _userManager.UpdateAsync(u);
                }
                else
                {
                    throw new Exception("User is not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserProfileDTO> GetUserProfile(string email)
        {
            var user = await _context.Users
                .Where(x => x.Email == email)
                .ProjectTo<UserProfileDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return user;
        }
        public async Task UpdateUserPhoto(UserPhotoDTO userPhotoDTO)
        {
            try
            {

                var userphoto = _mapper.Map<UserPhoto>(userPhotoDTO);
                _context.Update(userphoto);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserPhoto> GetUserPhotoByUserEmail(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user.UserPhoto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Transaction>> GetAllUserTransaction()
        {
            try
            {
                var transaction = await _context.Transactions.ToListAsync();
                return transaction;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Transaction>> GetUserTransaction(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user.TransactionSents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task AddTransaction(TransactionDTO transactionDTO)
        {
            try
            {
                var transaction = _mapper.Map<Transaction>(transactionDTO);
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateUserLastActive(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                user.LastActive = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UserProfileDTO>> GetSubberByCookId(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                List<UserProfileDTO> list = new List<UserProfileDTO>();
                var subs = await _context.AppUserSubscriptionRecords.Where(u => u.SourceUserId != user.Id).ToListAsync();
                foreach (var subbers in subs)
                {
                    list.Add(
                        await _context.Users
                        .Where(x => x.Id == subbers.SourceUserId)
                        .ProjectTo<UserProfileDTO>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync()
                        );
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task FollowUser(string cookEmail, string userEmail)
        {
            var cook = await _userManager.FindByEmailAsync(cookEmail);
            var user = await _userManager.FindByEmailAsync(userEmail);
            var follow = new UserFollowDTO
            {
                SourceUserId = user.Id,
                TargetUserId = cook.Id
            };
            var userfollow = _mapper.Map<UserFollow>(follow);
            await _context.UserFollows.AddAsync(userfollow);
            await _context.SaveChangesAsync();
        }

        public async Task<SubscriptionUserDTO> GetUserSubscription(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user.SubscriptionId == null)
            {
                return null;
            }
            var result = await _context.Users.Where(x => x.Email == email)
                .ProjectTo<SubscriptionUserDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return result;
        }

        public async Task UpdateUserSubscription(UpdateUserSubsciprtionDTO updateDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == updateDto.UserId);
            if (user != null)
            {
                var subscription = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Id == user.SubscriptionId);
                if (subscription != null)
                {
                    subscription.Duration = updateDto.Duration;
                    subscription.Price = updateDto.Price;
                }
                else
                {
                    var newSubscription = new Subscription
                    {
                        Duration = updateDto.Duration,
                        Price = updateDto.Price,
                    };
                    user.Subscription = newSubscription;
                }
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddPaymentIntent(long orderCode, int userId, int subscriptionId)
        {
            var payment = new SubscriptionPayment
            {
                OrderCode = orderCode,
                AppUserId = userId,
                SubscriptionId = subscriptionId,
                CreatedDate = DateTime.UtcNow,
            };
            await _context.SubscriptionPayments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Subscription> GetSubscriptionById(int id)
        {
            return await _context.Subscriptions.FindAsync(id);
        }

        public async Task<SubscriptionPayment> GetPaymentIntent(long orderCode)
        {
            return await _context.SubscriptionPayments.FirstOrDefaultAsync(x => x.OrderCode == orderCode);
        }

        public async Task<bool> HasLiked(string cookEmail, int userId)
        {
            var cook = await _context.Users.FirstOrDefaultAsync(x => x.Email == cookEmail);
            var result = false;
            result = await _context.UserFollows.AnyAsync(x => x.SourceUserId == userId && x.TargetUserId == cook.Id);
            return result;
        }

        public async Task SetTasteProfileUser(TasteProfileDTO tasteProfileDTO, string userEmail)
        {
            var user = await _context.Users.Where(u => u.Email == userEmail).FirstOrDefaultAsync();

            var usertaste = _mapper.Map<TasteProfile>(tasteProfileDTO);

            user.TasteProfile = usertaste;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }


        public async Task<List<UserMealPlanAdminDTO>> GetUserMealPlan()
        {
            List<UserMealPlanAdminDTO> list = null;
            try
            {
                var l = await _context.UserMealPlans.Include(m => m.AppUser).Include(m => m.MeanPlan).ToListAsync();
                list = _mapper.Map<List<UserMealPlanAdminDTO>>(l);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public AppUser GetCookWithRecipes(int cookId)
        {
            return _context.Users
                    .Include(u => u.Recipes)
                    .Include(u => u.UserSubscriptions)
                    .ThenInclude(us => us.Subscription)
                    .Include(u => u.SubByUsers)
                    .ThenInclude(asr => asr.Subscription)
                    .FirstOrDefault(u => u.Id == cookId);
        }

        public CookSalaryDTO CalculateCookSalary(int cookId)
        {
            const decimal MoneyPerThousandViews = 5000m;
            const decimal SubscriptionSharePercentage = 0.70m;

            var cook = GetCookWithRecipes(cookId);

            if (cook == null)
            {
                throw new Exception("Cook not found");
            }

            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month;



            try
            {
                var recipesInMonth = cook.Recipes
                    .Where(recipe => recipe.CreateDate.Year == year && recipe.CreateDate.Month == month)
                    .ToList();

                int totalViews = recipesInMonth.Sum(recipe => recipe.NumberOfViews);
                decimal moneyFromViews = (totalViews / 1000) * MoneyPerThousandViews;

                //Khuc' nay` t k biet phai query bang nao nen t set cung la cai duration -30 ngay la ra ngay dang ky
                decimal moneyFromSubscriptions = _context.AppUserSubscriptionRecords
                    .Where(asr => asr.TargetUserId == cookId
                                && asr.SubscriptionDuration.Year == year
                                && asr.SubscriptionDuration.AddDays(-30).Month == month) 
                    .Sum(asr => asr.Subscription.Price * SubscriptionSharePercentage);


                decimal totalMoney = moneyFromViews + moneyFromSubscriptions;

                return new CookSalaryDTO
                {
                    CookName = cook.KnownAs,
                    SalaryReportName = "Lương tháng " + month,
                    TotalMoneyReceived = totalMoney,
                    MoneyFromViews = moneyFromViews,
                    MoneyFromSubscriptions = moneyFromSubscriptions
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CookSalaryDTO>> GetCooksSalaries()
        {
            var cooks = await _context.Users.Where(user => user.UserRoles.Any(role => role.RoleId == 3)).ToListAsync();

            var cookSalaries = cooks.Select(cook => CalculateCookSalary(cook.Id)).ToList();

            return cookSalaries;
        }

    }
}
