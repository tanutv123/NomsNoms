using AutoMapper;
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
    }
}
