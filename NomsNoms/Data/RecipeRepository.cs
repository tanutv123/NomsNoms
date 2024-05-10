using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NomsNoms.DTOs;
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
        public async Task<PagedList<RecipeDTO>> GetRecipesAsync(UserParams userParams)
        {
            var query = _context.Recipes.Include(x => x.RecipeCategories).AsQueryable();
            query = userParams.OrderByCompletionTime switch
            {
                "asc" => query.OrderBy(x => x.CompletionTime),
                _ => query.OrderByDescending(x => x.CompletionTime)
            };
            if(userParams.CategoryId != 0)
            {
                query = query.Where(x => x.RecipeCategories.Any(x => x.CategoryId == userParams.CategoryId));

            }
            return await PagedList<RecipeDTO>.CreateAsync(query.AsNoTracking().ProjectTo<RecipeDTO>(_mapper.ConfigurationProvider),
                                                            userParams.PageNumber,
                                                            userParams.PageSize);
        }
    }
}
