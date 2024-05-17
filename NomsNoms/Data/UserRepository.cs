using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            }catch(Exception ex)
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
        public async Task Follow(string email,int userid)
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

        public async Task<bool> BuySubscription(string cookEmail, string email, int subscriptionId)
        {
            try
            {
                var sourceUser = await _userManager.FindByEmailAsync(email);
                var targetUser = await _userManager.FindByEmailAsync(cookEmail);
                var subscription = _context.Subscriptions.Where(S => S.Id == subscriptionId).FirstOrDefault();
                /*if (sourceUser.Money >= subscription.Price)
                {
                    sourceUser.SubscriptionId = subscription.Id;
                    await _context.SaveChangesAsync();

                    var record = new SubscriptionRecordDTO()
                    {
                        SourceUserId = sourceUser.Id,
                        TargetUserId = targetUser.Id,
                        SubscriptionId = subscription.Id,
                        SubscriptionDuration = DateTime.Now
                    };
                    await _context.AppUserSubscriptionRecords.AddAsync(_mapper.Map<AppUserSubscriptionRecord>(record));
                    await _context.SaveChangesAsync();

                    return true;
                }*/

                return false;
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
                .Include(u => u.TasteProfile)
                .SingleOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                // Handle the case where the user was not found
                return null;
            }

            return user.TasteProfile;
        }


        public async Task EnableUserAdmin(AppUser user)
        {
            try
            {
                var u = await _userManager.FindByEmailAsync(user.Email);
                if (u != null)
                {
                    u.Status = 1;
                    await _userManager.UpdateAsync(u);
                }
                else
                {
                    throw new Exception("User is not exist");
                }
            }catch(Exception ex)
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
            catch(Exception ex)
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
    }
}
