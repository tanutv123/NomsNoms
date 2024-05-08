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
        public static async Task SeedSubscription(DataContext _context)
        {
            if (await _context.Subscriptions.AnyAsync()) { return; }

            var subscription = await File.ReadAllTextAsync("../NomsNoms/Data/Seed/SubscriptionSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var sub = JsonSerializer.Deserialize<List<Subscription>>(subscription, jsonOptions);

            foreach (var subs in sub)
            {
                await _context.Subscriptions.AddAsync(subs);
                await _context.SaveChangesAsync();
            }
        }

        public static async Task SeedMealPlanType(DataContext _context)
        {
            if(await _context.MealPlanTypes.AnyAsync()) { return; }

            var mealPlanType = await File.ReadAllTextAsync("../NomsNoms/Data/Seed/MealPlanType.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var mpt = JsonSerializer.Deserialize<List<MealPlanType>>(mealPlanType, jsonOptions);

            foreach(var mealType in mpt)
            {
                await _context.MealPlanTypes.AddAsync(mealType);
                await _context.SaveChangesAsync();
            }
        }
        public static async Task SeedMealPlan(DataContext _context)
        {
            if (await _context.MealPlans.AnyAsync()) { return; }

            var mealPlan = await File.ReadAllTextAsync("../NomsNoms/Data/Seed/MealPlanSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var mp = JsonSerializer.Deserialize<List<MealPlan>>(mealPlan, jsonOptions);

            foreach (var mealPlans in mp)
            {
                await _context.MealPlans.AddAsync(mealPlans);
                await _context.SaveChangesAsync();
            }
        }

        public static async Task SeedIngredient(DataContext _context)
        {
            if(await _context.Ingredients.AnyAsync()) { return; }

            var ingredient = await File.ReadAllTextAsync("../NomsNoms/Data/Seed/IngredientSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var ing = JsonSerializer.Deserialize<List<Ingredient>>(ingredient, jsonOptions);

            foreach(var i in ing)
            {
                await _context.Ingredients.AddAsync(i);
                await _context.SaveChangesAsync();
            }
        }
    }
}
