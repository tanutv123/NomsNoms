﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NomsNoms.DTOs;
using NomsNoms.Entities;
using NomsNoms.Interfaces;

namespace NomsNoms.Data
{
    public class MealPlanRepository : IMealPlanRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public MealPlanRepository(DataContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<List<MealPlanType>> GetAllType()
        {
            List<MealPlanType> list = null;
            try
            {
                list = await _context.MealPlanTypes.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
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

            // Sort the list by similarity score in descending order
            recipeScores.Sort((x, y) => y.Item2.CompareTo(x.Item2));

            // Take the top 3 recipes
            var topRecipes = recipeScores.Take(3).Select(item => item.Item1).ToList();

            return topRecipes;
        }

        public async Task<List<MealPlanSubscription>> GetMealPlanSubscriptionsAsync()
        {
            return await _context.MealPlanSubscriptions.ToListAsync();
        }
        public async Task RegistMealPlan(int userId, int mealplanid)
        {
            var existedMealPlan = await _context.UserMealPlanSubscriptions
                .Where(x => x.AppUserId == userId)
                .ToListAsync();
            if(existedMealPlan != null)
            {
                foreach (var subscription in existedMealPlan)
                {
                    _context.UserMealPlanSubscriptions.Remove(subscription);
                }
            }
            var user_mealplan = new UserMealplanDTO
            {
                AppUserId = userId,
                MealPlanSubscriptionId = mealplanid,
                StartedDate = DateTime.Now
            };
            var mealplan_regist = _mapper.Map<UserMealPlanSubscriptions>(user_mealplan);
            await _context.UserMealPlanSubscriptions.AddAsync(mealplan_regist);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsRegisterMealPlan(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            var result = false;
            if(user != null)
            {
                var userRegisteredMealPlan = await _context.UserMealPlanSubscriptions.FirstOrDefaultAsync(x => x.AppUserId == user.Id);
                if(userRegisteredMealPlan != null)
                {
                    var mealPlan = await _context.MealPlanSubscriptions.FirstOrDefaultAsync(x => x.Id == userRegisteredMealPlan.MealPlanSubscriptionId);
                    result = IsValidMealPlanSubscription(userRegisteredMealPlan, mealPlan);
                }
            }
            return result;
        }
        private bool IsValidMealPlanSubscription(UserMealPlanSubscriptions userRegisteredMealPlan, MealPlanSubscription mealPlan)
        {
            DateTime expiryDate = userRegisteredMealPlan.StartedDate.AddDays(mealPlan.Duration);

            if (DateTime.UtcNow < expiryDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<MealPlanSubscription> GetMealPlanSubscriptionAsync(int id)
        {
            return await _context.MealPlanSubscriptions.FirstOrDefaultAsync(x => x.Id == id && x.Status == 1);
        }

        public async Task AddPaymentIntent(long orderCode, int userId, int mealPlanId)
        {
            var mealPlanPayment = new MealPlanPayment
            {
                OrderCode = orderCode,
                AppUserId = userId,
                MealPlanId = mealPlanId,
                CreatedDate = DateTime.UtcNow
            };
            await _context.MealPlanPayments.AddAsync(mealPlanPayment);
            await _context.SaveChangesAsync();
        }

        public async Task<MealPlanPayment> GetPaymentIntent(long orderCode)
        {
            return await _context.MealPlanPayments.FirstOrDefaultAsync(x => x.OrderCode == orderCode);
        }

        public async Task CreateMealPlan(MealPlanAdminDTO mealPlan)
        {
            try
            {
                var mp = await _context.MealPlanSubscriptions.FirstOrDefaultAsync(m => m.Id == mealPlan.Id);
                if(mp == null)
                {

                    var mealPlanEntity = new MealPlanSubscription
                    {
                        Price = mealPlan.Price,
                        Duration = mealPlan.Duration,
                        CreatedDate = mealPlan.CreatedDate,
                        Description = mealPlan.Description,
                        Status = 1
                    };

                    await _context.AddAsync(mealPlanEntity);
                    await _context.SaveChangesAsync();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateMealPlan(MealPlanAdminDTO mealPlan)
        {
            try
            {
                var mp = await _context.MealPlanSubscriptions.FirstOrDefaultAsync(m => m.Id == mealPlan.Id);
                if (mp != null)
                {
                    mp.Price = mealPlan.Price;
                    mp.Duration = mealPlan.Duration;
                    mp.Description = mealPlan.Description;
                    mp.CreatedDate = mealPlan.CreatedDate;
                    mp.Status = mealPlan.Status;        
                    _context.Update(mp);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task DeleteMealPlan(MealPlanAdminDTO mealPlan)
        {
            try
            {
                var mp = await _context.MealPlanSubscriptions.FirstOrDefaultAsync(m => m.Id == mealPlan.Id);
                if (mp != null)
                {
                    mp.Status = 0; 

                    _context.Update(mp);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MealPlanAdminDTO>> GetMealPlansAdmin()
        {
            var result = await _context.MealPlanSubscriptions
                .ProjectTo<MealPlanAdminDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }

        public async Task<MealPlanAdminDTO> GetMealPlanAdmin(int id)
        {
            var result = await _context.MealPlanSubscriptions
                .Where(x => x.Id == id)
                .ProjectTo<MealPlanAdminDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return result; 
        }

        public async Task EnableMealPlan(MealPlanAdminDTO mealPlan)
        {
            try
            {
                var mp = await _context.MealPlanSubscriptions.FirstOrDefaultAsync(m => m.Id == mealPlan.Id);
                if (mp != null)
                {
                    mp.Status = 1;

                    _context.Update(mp);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
