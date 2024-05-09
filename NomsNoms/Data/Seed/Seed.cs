using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NomsNoms.Entities;
using System.Data;
using System.Text.Json;

namespace NomsNoms.Data.Seed
{
    public class Seed
    {
        public static async Task SeedUser(UserManager<AppUser> _userManager, RoleManager<AppRole> _roleManager)
        {
            if (await _userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("../NomsNoms/Data/Seed/UserSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData, jsonOptions);

            var role = new List<AppRole>
            {
                new AppRole {Name = "Client"},
                new AppRole {Name = "Staff"},
                new AppRole {Name = "Cook"},
                new AppRole {Name = "Manager"},
                new AppRole {Name = "Admin"}
            };

            foreach (var roles in role)
            {
                await _roleManager.CreateAsync(roles);
            }

            foreach (var user in users)
            {
                user.UserName = user.Email;
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, "Pa$$w0rd");
                await _userManager.AddToRoleAsync(user, "Client");
            }

            var admin = new AppUser
            {
                UserName = "admin@gmail.com",
                KnownAs = "admin",
                Email = "admin@gmail.com",
                CreatedDate = DateTime.Now,
                LastActive = DateTime.Now,
                EmailConfirmed = true,
            };

            var resultAdmin = await _userManager.CreateAsync(admin, "Pa$$w0rd");
            await _userManager.AddToRolesAsync(admin, new[] { "Admin" });

            var manager = new AppUser
            {
                UserName = "manager@gmail.com",
                KnownAs = "manager",
                Email = "manager@gmail.com",
                CreatedDate = DateTime.Now,
                LastActive = DateTime.Now,
                EmailConfirmed = true,
            };

            await _userManager.CreateAsync(manager, "Pa$$w0rd");
            await _userManager.AddToRolesAsync(manager, new[] { "Manager" });

            var cook = new AppUser
            {
                UserName = "gordonramsay@gmail.com",
                KnownAs = "Gordon Ramsay",
                Email = "gordonramsay@gmail.com",
                PhoneNumber = "+89 (895) 538-2282",
                CreatedDate = DateTime.Now,
                LastActive = DateTime.Now,
                Introduction = "I am the best Chef of the world! You donut!!!!",
                EmailConfirmed = true,
            };

            await _userManager.CreateAsync(cook, "Pa$$w0rd");
            await _userManager.AddToRoleAsync(cook, "Cook");

            var staff = new AppUser
            {
                UserName = "staff@gmail.com",
                KnownAs = "Thang Jack",
                Email = "staff@gmail.com",
                CreatedDate = DateTime.Now,
                LastActive = DateTime.Now,
                EmailConfirmed = true,
            };

            await _userManager.CreateAsync(staff, "Pa$$w0rd");
            await _userManager.AddToRoleAsync(staff, "Staff");
        }

        public static async Task SeedCategory(DataContext _context)
        {
            if (await _context.Categories.AnyAsync()) { return; }

            var categories = await File.ReadAllTextAsync("../NomsNoms/Data/Seed/CategorySeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var cate = JsonSerializer.Deserialize<List<Category>>(categories, jsonOptions);

            foreach (var category in cate)
            {
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
            }
        }
        public static async Task SeedIngredient(DataContext _context)
        {
            if (await _context.Ingredients.AnyAsync()) { return; }

            var ingredients = await File.ReadAllTextAsync("../NomsNoms/Data/Seed/IngredientSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var ingre = JsonSerializer.Deserialize<List<Ingredient>>(ingredients, jsonOptions);

            foreach (var ingredient in ingre)
            {
                await _context.Ingredients.AddAsync(ingredient);
                await _context.SaveChangesAsync();
            }
        }
        public static async Task SeedRecipe(DataContext _context)
        {
            if (await _context.Recipes.AnyAsync()) { return; }

            var recipes = await File.ReadAllTextAsync("../NomsNoms/Data/Seed/RecipeSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var rep = JsonSerializer.Deserialize<List<Recipe>>(recipes, jsonOptions);

            var status = new List<RecipeStatus>
            {
                new RecipeStatus {Name = "Hidden"},
                new RecipeStatus {Name = "Normal"},
                new RecipeStatus {Name = "Pending"},
                new RecipeStatus {Name = "Deleted"}
            };

            foreach (var state in status)
            {
                await _context.RecipeStatuses.AddAsync(state);
            }

            foreach (var recipe in rep)
            {
                await _context.Recipes.AddAsync(recipe);
                await _context.SaveChangesAsync();
            }
        }
    }
}
