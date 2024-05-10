﻿using AutoMapper;
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
                        AppUserId = user.Id,
                        FollowerCount = countFollower,
                        Name = user.UserName,
                        KnownAs = user.KnownAs                  
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

    }
}
