using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Helpers;
using NomsNoms.Interfaces;

namespace NomsNoms.Data
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public RecipeRepository(DataContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<RecipeDTO> GetRecipeAsync(int id)
        {
            return await _context.Recipes.Where(x => x.Id == id)
                .ProjectTo<RecipeDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedList<RecipeDTO>> GetRecipesAsync(UserParams userParams)
        {
            var query = _context.Recipes.Include(x => x.RecipeCategories).AsQueryable();
            if (!string.IsNullOrEmpty(userParams.Search))
                query = query.Where(x => x.Title.Contains(userParams.Search));
            query = userParams.OrderByCompletionTime switch
            {
                "asc" => query.OrderBy(x => x.CompletionTime),
                _ => query.OrderByDescending(x => x.CompletionTime)
            };
            if (userParams.CategoryIds != null && userParams.CategoryIds.Length != 0)
            {
                foreach(var categoryId in userParams.CategoryIds)
                {
                    query = query.Where(x => x.RecipeCategories.Any(x => userParams.CategoryIds.Contains(x.CategoryId)));
                }
            }
            return await PagedList<RecipeDTO>.CreateAsync(query.AsNoTracking().ProjectTo<RecipeDTO>(_mapper.ConfigurationProvider),
                                                            userParams.PageNumber,
                                                            userParams.PageSize);
        }

        public async Task<List<RecipeDTO>> GetTrendingRecipe()
        {
            var query = await _context.Recipes
                .OrderByDescending(r => r.RecipeLikes.Count)
                .Take(5)
                .ToListAsync();
            return _mapper.Map<List<RecipeDTO>>(query);
        }
        public async Task Like(string email, int recipeid)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var cookId = user.Id;
                var isFollow = await _context.RecipeLikes.AnyAsync(a => a.AppUserId == cookId && a.RecipeId == recipeid);
                if (cookId != null && recipeid != null && isFollow)
                {
                    var index = await _context.RecipeLikes.Where(a => a.AppUserId == cookId && a.RecipeId == recipeid).FirstOrDefaultAsync();
                    _context.RecipeLikes.Remove(index);
                    await _context.SaveChangesAsync();
                }
                else if (cookId != null && recipeid != null)
                {
                    var recipe = new RecipeLike
                    {
                        AppUserId = cookId,
                        RecipeId = recipeid
                    };
                    await _context.RecipeLikes.AddAsync(recipe);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<RecipeLikeToShowDTO>> GetRecipeLikeByUserEmail(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var recipeLikes = await _context.RecipeLikes.Where(r => r.AppUserId == user.Id).ToListAsync();
                List<RecipeLikeToShowDTO> recipelikes = new List<RecipeLikeToShowDTO>();
                foreach (var recipe in recipeLikes)
                {
                    var recipe2 = await _context.Recipes.Where(r => r.Id == recipe.RecipeId)
                        .Include(u => u.RecipeImage)
                        .Include(s => s.RecipeStatus)
                        .Include(u => u.AppUser)
                        .FirstOrDefaultAsync();
                    var likedRecipe = new RecipeLikeToShowDTO()
                    {
                        Id = recipe2.Id,
                        Title = recipe2.Title,
                        Description = recipe2.Description,
                        RecipeStatusName = recipe2.RecipeStatus.Name,
                        UserKnownAs = recipe2.AppUser.KnownAs,
                        IsExclusive = recipe2.IsExclusive,
                        RecipeImageUrl = recipe2.RecipeImage.Url,
                        NumberOfViews = recipe2.NumberOfViews
                    };
                    recipelikes.Add(likedRecipe);

                }
                return recipelikes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
