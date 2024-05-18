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

        public async Task<List<Recipe>> GetAllRecipes()
        {
            List<Recipe> list = null;
            try
            {
                list = await _context.Recipes.Include(u => u.TastProfile).ToListAsync();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public async Task<List<RecipeDTO>> GetAllRecipeAdmin()
        {
            List<RecipeDTO> list = null;
            try
            {
                var l = await _context.Recipes.ToListAsync();
                list = _mapper.Map<List<RecipeDTO>>(l);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public async Task<RecipeUpdateDTO> GetRecipeById(int id)
        {
            RecipeUpdateDTO recipe = null;
            try
            {
                var r = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
                recipe = _mapper.Map<RecipeUpdateDTO>(r);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return recipe;
        }


        public async Task UpdateRecipe(RecipeUpdateDTO recipeDto)
        {
            try
            {
                var existingRecipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeDto.Id);
                if (existingRecipe != null)
                {
                    existingRecipe.RecipeStatusId = recipeDto.RecipeStatusId;
                    _mapper.Map(recipeDto, existingRecipe);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Recipe with Id {recipeDto.Id} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteRecipe(RecipeUpdateDTO recipeDto)
        {
            try
            {
                var existingRecipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeDto.Id);
                if (existingRecipe != null)
                {
                    recipeDto.RecipeStatusId = 4;
                    _mapper.Map(recipeDto, existingRecipe);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Recipe with Id {recipeDto.Id} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RecipeStatus>> GetAllRecipeStatus()
        {
            List<RecipeStatus> list = null;
            try
            {
                list = await _context.RecipeStatuses.ToListAsync();
            }catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
            return list;
        }
        public async Task<List<RecipeDTO>> GetAllPendingRecipeAdmin()
        {
            List<RecipeDTO> list = null;
            try
            {
                var l = await _context.Recipes.Where(r => r.RecipeStatusId == 3).ToListAsync();
                list = _mapper.Map<List<RecipeDTO>>(l);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public async Task<TasteProfile> GetTasteProfileById(int id)
        {
            TasteProfile tp = null;
            try
            {
                tp = await _context.TasteProfiles.FirstOrDefaultAsync(t => t.Id == id);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tp;
        }

        public async Task SetTatseProfileAndStatus(int recipeId, TasteProfile tp)
        {
            try
            {
                var profile = await GetTasteProfileById(tp.Id);
                if(profile == null)
                {
                    await _context.TasteProfiles.AddAsync(tp);
                    await _context.SaveChangesAsync();

                    var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
                    if(recipe != null || recipe.RecipeStatusId != 3)
                    {
                        recipe.TasteProfileId = tp.Id;
                        recipe.RecipeStatusId = 1;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Recipe not found");
                    }
                }
                else
                {
                    throw new Exception("TasteProfile is existed");
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<List<RecipeDTO>> GetRecipeForUser(int id)
        {
            var query = _context.Recipes.AsQueryable();
            query = query.Where(x => x.AppUserId == id);
            query = query.Where(x => x.RecipeStatusId != 4);
            return await query.ProjectTo<RecipeDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<RecipeDTO>> GetUserRecipeForProfile(string email)
        {
            var query = _context.Recipes.AsQueryable();
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            query = query.Where(x => x.AppUserId == user.Id);
            query = query.Where(x => x.RecipeStatusId != 4 && x.RecipeStatusId != 1);
            return await query.ProjectTo<RecipeDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }


        public async Task HideRecipe(int recipeid)
        {
            try
            {
                var recipe = await _context.Recipes.Where(r => r.Id == recipeid).FirstOrDefaultAsync();
                if(recipe.RecipeStatusId == 3)
                {
                    throw new Exception("Công thức đang chờ duyệt");
                }
                if (recipe.RecipeStatusId == 1)
                {
                    recipe.RecipeStatusId = 2;
                    _context.Recipes.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                else if (recipe.RecipeStatusId == 2)
                {
                    recipe.RecipeStatusId = 1;
                    _context.Recipes.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                else if (recipe.RecipeStatusId == 4)
                {
                    throw new Exception("Bạn không thể mở công thức đã bị xóa.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeletedRecipe(int recipeid)
        {
            try
            {
                var recipe = await _context.Recipes.Where(r => r.Id == recipeid).FirstOrDefaultAsync();
                recipe.RecipeStatusId = 4;
                _context.Recipes.Update(recipe);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> IsOwnerRecipe(int recipeid, string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                var recipe = await _context.Recipes.Where(r => r.Id == recipeid).FirstOrDefaultAsync();
                if (user != null && recipe != null && recipe.AppUserId == user.Id)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ViewRecipeAdminDTO> GetRecipeAdminAsync(int id)
        {
            return await _context.Recipes.Where(x => x.Id == id).ProjectTo<ViewRecipeAdminDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<bool> IsLike(string email, int recipeid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            var result = await _context.RecipeLikes.AnyAsync(x => x.RecipeId == recipeid && x.AppUserId == user.Id);
            return result;
        }

        public double CalculateSimilarity(TasteProfile userTaste, TasteProfile recipeTaste)
        {
            if (userTaste == null || recipeTaste == null)
            {
                return 0; // or handle it appropriately
            }

            // Using cosine similarity as an example
            double dotProduct = (userTaste.Spiciness * recipeTaste.Spiciness) +
                                (userTaste.Sweetness * recipeTaste.Sweetness) +
                                (userTaste.Saltiness * recipeTaste.Saltiness);
            // ... include other attributes in the dot product calculation

            double userMagnitude = Math.Sqrt(userTaste.Spiciness * userTaste.Spiciness +
                                             userTaste.Sweetness * userTaste.Sweetness +
                                             userTaste.Saltiness * userTaste.Saltiness);
            // ... include other attributes in the magnitude calculation for the user

            double recipeMagnitude = Math.Sqrt(recipeTaste.Spiciness * recipeTaste.Spiciness +
                                               recipeTaste.Sweetness * recipeTaste.Sweetness +
                                               recipeTaste.Saltiness * recipeTaste.Saltiness);
            // ... include other attributes in the magnitude calculation for the recipe

            if (userMagnitude == 0 || recipeMagnitude == 0)
            {
                return 0; // To handle division by zero
            }

            return dotProduct / (userMagnitude * recipeMagnitude);
        }

        public async Task<List<Recipe>> RecommendRecipes(TasteProfile userTaste, List<Recipe> allRecipes)
        {
            // Create a new list to store the recipes and their scores
            List<(Recipe, double)> recipeScores = new List<(Recipe, double)>();

            // Loop through all the recipes
            foreach (var recipe in allRecipes)
            {
                // Ensure that the recipe and its taste profile are not null
                if (recipe != null && recipe.TastProfile != null)
                {
                    // Calculate the similarity score for the current recipe
                    double similarityScore = CalculateSimilarity(userTaste, recipe.TastProfile);

                    // Add the recipe and its similarity score to the list
                    recipeScores.Add((recipe, similarityScore));
                }
            }

            // Take the top recipes
            var topRecipes = recipeScores.Select(item => item.Item1).ToList();

            return topRecipes;
            }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            recipe.RecipeStatusId = 3;
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
        }
    }
}
