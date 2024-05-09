﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NomsNoms.Data;

#nullable disable

namespace NomsNoms.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NomsNoms.Entities.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("NomsNoms.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Introduction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KnownAs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SubscriptionId")
                        .HasColumnType("int");

                    b.Property<int?>("TasteProfileId")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SubscriptionId");

                    b.HasIndex("TasteProfileId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("NomsNoms.Entities.AppUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("NomsNoms.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("NomsNoms.Entities.CollectionRecipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("UserCollectionId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "UserCollectionId");

                    b.HasIndex("UserCollectionId");

                    b.ToTable("CollectionRecipes");
                });

            modelBuilder.Entity("NomsNoms.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Calories")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("NomsNoms.Entities.MealPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("MealPlanTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("MealPlanTypeId");

                    b.ToTable("MealPlans");
                });

            modelBuilder.Entity("NomsNoms.Entities.MealPlanIngredient", b =>
                {
                    b.Property<int>("MealPlanId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.HasKey("MealPlanId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("MealPlanIngredients");
                });

            modelBuilder.Entity("NomsNoms.Entities.MealPlanType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MealPlanTypes");
                });

            modelBuilder.Entity("NomsNoms.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Saltiness")
                        .HasColumnType("int");

                    b.Property<int>("Sauce")
                        .HasColumnType("int");

                    b.Property<int>("Spiciness")
                        .HasColumnType("int");

                    b.Property<int>("Sweetness")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("NomsNoms.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsExclusive")
                        .HasColumnType("bit");

                    b.Property<int?>("RecipeImageId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeStatusId")
                        .HasColumnType("int");

                    b.Property<int?>("TasteProfileId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("RecipeImageId");

                    b.HasIndex("RecipeStatusId");

                    b.HasIndex("TasteProfileId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeCategory", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("RecipeCategories");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecipeStepId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RecipeStepId");

                    b.ToTable("RecipeImages");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeLike", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "AppUserId");

                    b.HasIndex("AppUserId");

                    b.ToTable("RecipeLikes");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RecipeStatuses");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("No")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeSteps");
                });

            modelBuilder.Entity("NomsNoms.Entities.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("NomsNoms.Entities.TasteProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Saltiness")
                        .HasColumnType("int");

                    b.Property<int>("Sauce")
                        .HasColumnType("int");

                    b.Property<int>("Spiciness")
                        .HasColumnType("int");

                    b.Property<int>("Sweetness")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TasteProfiles");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("UserCollections");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserFavorite", b =>
                {
                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("AppUserId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("UserFavorites");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserFollow", b =>
                {
                    b.Property<int>("SourceUserId")
                        .HasColumnType("int");

                    b.Property<int>("TargetUserId")
                        .HasColumnType("int");

                    b.HasKey("SourceUserId", "TargetUserId");

                    b.HasIndex("TargetUserId");

                    b.ToTable("UserFollows");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserMealPlan", b =>
                {
                    b.Property<int>("MealPlanId")
                        .HasColumnType("int");

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MealPlanId", "AppUserId");

                    b.HasIndex("AppUserId");

                    b.ToTable("UserMealPlans");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<string>("PublicId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId")
                        .IsUnique();

                    b.ToTable("UserPhotos");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserSubscription", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .HasColumnType("int");

                    b.Property<int>("AppUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SubscriptionId", "AppUserId");

                    b.HasIndex("AppUserId");

                    b.ToTable("UserSubscriptions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NomsNoms.Entities.AppUser", b =>
                {
                    b.HasOne("NomsNoms.Entities.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId");

                    b.HasOne("NomsNoms.Entities.TasteProfile", "TasteProfile")
                        .WithMany()
                        .HasForeignKey("TasteProfileId");

                    b.Navigation("Subscription");

                    b.Navigation("TasteProfile");
                });

            modelBuilder.Entity("NomsNoms.Entities.AppUserRole", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppRole", "AppRole")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NomsNoms.Entities.AppUser", "AppUser")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppRole");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("NomsNoms.Entities.CollectionRecipe", b =>
                {
                    b.HasOne("NomsNoms.Entities.Recipe", "Recipe")
                        .WithMany("CollectionRecipes")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NomsNoms.Entities.UserCollection", "UserCollection")
                        .WithMany("CollectionRecipes")
                        .HasForeignKey("UserCollectionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("UserCollection");
                });

            modelBuilder.Entity("NomsNoms.Entities.MealPlan", b =>
                {
                    b.HasOne("NomsNoms.Entities.MealPlanType", "MealPlanType")
                        .WithMany()
                        .HasForeignKey("MealPlanTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealPlanType");
                });

            modelBuilder.Entity("NomsNoms.Entities.MealPlanIngredient", b =>
                {
                    b.HasOne("NomsNoms.Entities.Ingredient", "Ingredient")
                        .WithMany("MealPlanIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NomsNoms.Entities.MealPlan", "MeanPlan")
                        .WithMany("MealPlanIngredients")
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("MeanPlan");
                });

            modelBuilder.Entity("NomsNoms.Entities.Recipe", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppUser", "AppUser")
                        .WithMany("Recipes")
                        .HasForeignKey("AppUserId");

                    b.HasOne("NomsNoms.Entities.RecipeImage", "RecipeImage")
                        .WithMany()
                        .HasForeignKey("RecipeImageId");

                    b.HasOne("NomsNoms.Entities.RecipeStatus", "RecipeStatus")
                        .WithMany("Recipes")
                        .HasForeignKey("RecipeStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NomsNoms.Entities.TasteProfile", "TastProfile")
                        .WithMany()
                        .HasForeignKey("TasteProfileId");

                    b.Navigation("AppUser");

                    b.Navigation("RecipeImage");

                    b.Navigation("RecipeStatus");

                    b.Navigation("TastProfile");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeCategory", b =>
                {
                    b.HasOne("NomsNoms.Entities.Category", "Category")
                        .WithMany("RecipeCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NomsNoms.Entities.Recipe", "Recipe")
                        .WithMany("RecipeCategories")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeImage", b =>
                {
                    b.HasOne("NomsNoms.Entities.RecipeStep", null)
                        .WithMany("RecipeStepImages")
                        .HasForeignKey("RecipeStepId");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeIngredient", b =>
                {
                    b.HasOne("NomsNoms.Entities.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredient")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NomsNoms.Entities.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeLike", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppUser", "AppUser")
                        .WithMany("LikedRecipes")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NomsNoms.Entities.Recipe", "Recipe")
                        .WithMany("RecipeLikes")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeStep", b =>
                {
                    b.HasOne("NomsNoms.Entities.Recipe", "Recipe")
                        .WithMany("RecipeSteps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserCollection", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppUser", "AppUser")
                        .WithMany("UserCollections")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserFavorite", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppUser", "AppUser")
                        .WithMany("UserFavorites")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NomsNoms.Entities.Category", "Category")
                        .WithMany("UserFavorites")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserFollow", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppUser", "SourceUser")
                        .WithMany("FollowedUsers")
                        .HasForeignKey("SourceUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NomsNoms.Entities.AppUser", "TargetUser")
                        .WithMany("FollowedByUsers")
                        .HasForeignKey("TargetUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SourceUser");

                    b.Navigation("TargetUser");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserMealPlan", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppUser", "AppUser")
                        .WithMany("UserMealPlans")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NomsNoms.Entities.MealPlan", "MeanPlan")
                        .WithMany("UserMealPlans")
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("MeanPlan");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserPhoto", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppUser", "AppUser")
                        .WithOne("UserPhoto")
                        .HasForeignKey("NomsNoms.Entities.UserPhoto", "AppUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserSubscription", b =>
                {
                    b.HasOne("NomsNoms.Entities.AppUser", "AppUser")
                        .WithMany("UserSubscriptions")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("NomsNoms.Entities.Subscription", "Subscription")
                        .WithMany("UserSubscriptions")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppUser");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("NomsNoms.Entities.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("NomsNoms.Entities.AppUser", b =>
                {
                    b.Navigation("FollowedByUsers");

                    b.Navigation("FollowedUsers");

                    b.Navigation("LikedRecipes");

                    b.Navigation("Recipes");

                    b.Navigation("UserCollections");

                    b.Navigation("UserFavorites");

                    b.Navigation("UserMealPlans");

                    b.Navigation("UserPhoto");

                    b.Navigation("UserRoles");

                    b.Navigation("UserSubscriptions");
                });

            modelBuilder.Entity("NomsNoms.Entities.Category", b =>
                {
                    b.Navigation("RecipeCategories");

                    b.Navigation("UserFavorites");
                });

            modelBuilder.Entity("NomsNoms.Entities.Ingredient", b =>
                {
                    b.Navigation("MealPlanIngredients");

                    b.Navigation("RecipeIngredient");
                });

            modelBuilder.Entity("NomsNoms.Entities.MealPlan", b =>
                {
                    b.Navigation("MealPlanIngredients");

                    b.Navigation("UserMealPlans");
                });

            modelBuilder.Entity("NomsNoms.Entities.Recipe", b =>
                {
                    b.Navigation("CollectionRecipes");

                    b.Navigation("RecipeCategories");

                    b.Navigation("RecipeIngredients");

                    b.Navigation("RecipeLikes");

                    b.Navigation("RecipeSteps");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeStatus", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("NomsNoms.Entities.RecipeStep", b =>
                {
                    b.Navigation("RecipeStepImages");
                });

            modelBuilder.Entity("NomsNoms.Entities.Subscription", b =>
                {
                    b.Navigation("UserSubscriptions");
                });

            modelBuilder.Entity("NomsNoms.Entities.UserCollection", b =>
                {
                    b.Navigation("CollectionRecipes");
                });
#pragma warning restore 612, 618
        }
    }
}
