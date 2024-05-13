using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public RecipeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<List<RecipeStepDTO>> GetRecipeStepAsync(int id)
        {
            return await _context.RecipeSteps.Where(x => x.RecipeId == id).ProjectTo<RecipeStepDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<RecipeDTO>> GetTrendingRecipe()
        {
            var query = await _context.Recipes
                .OrderByDescending(r => r.RecipeLikes.Count)
                .Take(10)
                .ProjectTo<RecipeDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return query;
        }
    }
}
