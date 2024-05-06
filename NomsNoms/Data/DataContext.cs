using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NomsNoms.Entities;
using Microsoft.EntityFrameworkCore;

namespace NomsNoms.Data
{
    public class DataContext : IdentityDbContext<AppUser,
                                                AppRole,
                                                int,
                                                IdentityUserClaim<int>,
                                                AppUserRole,
                                                IdentityUserLogin<int>,
                                                IdentityRoleClaim<int>,
                                                IdentityUserToken<int>
                                                >
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUserRole> AppUserRole { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserCollection> UserCollections{ get; set; }
        public DbSet<CollectionRecipe> CollectionRecipes{ get; set; }
        public DbSet<MealPlan> MealPlans{ get; set; }
        public DbSet<MealPlanType> MealPlanTypes{ get; set; }
        public DbSet<Recipe> Recipes{ get; set; }
        public DbSet<RecipeCategory> RecipeCategories{ get; set; }
        public DbSet<RecipeImage> RecipeImages{ get; set; }
        public DbSet<RecipeLike> RecipeLikes{ get; set; }
        public DbSet<RecipeStatus> RecipeStatuses{ get; set; }
        public DbSet<RecipeStep> RecipeSteps{ get; set; }
        public DbSet<Subscription> Subscriptions{ get; set; }
        public DbSet<UserFollow> UserFollows{ get; set; }
        public DbSet<UserMealPlan> UserMealPlans{ get; set; }
        public DbSet<UserPhoto> UserPhotos{ get; set; }
        public DbSet<UserSubscription> UserSubscriptions{ get; set; }
        public DbSet<Question> Questions{ get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.AppUser)
                .HasForeignKey(u => u.UserId)
                .IsRequired();
            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(ur => ur.AppRole)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
            builder.Entity<UserFavorite>()
                .HasKey(k => new { k.AppUserId, k.CategoryId });
            builder.Entity<UserFavorite>()
                .HasOne(uf => uf.AppUser)
                .WithMany(u => u.UserFavorites)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserFavorite>()
                .HasOne(uf => uf.Category)
                .WithMany(c => c.UserFavorites)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<RecipeLike>()
                .HasKey(k => new {k.RecipeId, k.AppUserId});
            builder.Entity<RecipeLike>()
                .HasOne(a => a.Recipe)
                .WithMany(a => a.RecipeLikes)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RecipeLike>()
                .HasOne(a => a.AppUser)
                .WithMany(u => u.LikedRecipes)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<RecipeCategory>()
                .HasKey(k => new { k.RecipeId, k.CategoryId });
            builder.Entity<RecipeCategory>()
                .HasOne(rc => rc.Recipe)
                .WithMany(r => r.RecipeCategories)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<RecipeCategory>()
                .HasOne(rc => rc.Category)
                .WithMany(r => r.RecipeCategories)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<CollectionRecipe>()
                .HasKey(k => new { k.RecipeId, k.UserCollectionId });
            builder.Entity<CollectionRecipe>()
                .HasOne(cr => cr.Recipe)
                .WithMany(r => r.CollectionRecipes)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<CollectionRecipe>()
                .HasOne(cr => cr.UserCollection)
                .WithMany(uc => uc.CollectionRecipes)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserSubscription>()
                .HasKey(k => new { k.SubscriptionId, k.AppUserId });
            builder.Entity<UserSubscription>()
                .HasOne(us => us.Subscription)
                .WithMany(s => s.UserSubscriptions)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserSubscription>()
                .HasOne(us => us.AppUser)
                .WithMany(u => u.UserSubscriptions)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserMealPlan>()
                .HasKey(k => new { k.MealPlanId, k.AppUserId });
            builder.Entity<UserMealPlan>()
                .HasOne(ump => ump.MeanPlan)
                .WithMany(mp => mp.UserMealPlans)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserMealPlan>()
                .HasOne(ump => ump.AppUser)
                .WithMany(u => u.UserMealPlans)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<UserFollow>()
                .HasKey(k => new { k.SourceUserId, k.TargetUserId });
            builder.Entity<UserFollow>()
                .HasOne(fl => fl.SourceUser)
                .WithMany(u => u.FollowedUsers)
                .HasForeignKey(u => u.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<UserFollow>()
                .HasOne(fl => fl.TargetUser)
                .WithMany(u => u.FollowedByUsers)
                .HasForeignKey(u => u.TargetUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
