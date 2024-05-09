using AutoMapper;
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
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
    }
}
