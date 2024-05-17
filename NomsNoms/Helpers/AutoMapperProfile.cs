﻿using AutoMapper;
using NomsNoms.DTOs;
using NomsNoms.Entities;

namespace NomsNoms.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<AppUser, UserDTO>().ReverseMap();
            CreateMap<AppUser, UserAdminDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().AppRole.Name))
                .ReverseMap();
            CreateMap<AppUser, UserProfileDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UserPhoto.Url))
                .ReverseMap();
            CreateMap<Recipe, RecipeDTO>()
                .ForMember(dest => dest.RecipeStatusName, opt => opt.MapFrom(src => src.RecipeStatus.Name))
                .ForMember(dest => dest.RecipeComplexityName, opt => opt.MapFrom(src => src.RecipeComplexity.Name))
                .ForMember(dest => dest.RecipeImageUrl, opt => opt.MapFrom(src => src.RecipeImage.Url))
                .ForMember(dest => dest.UserKnownAs, opt => opt.MapFrom(src => src.AppUser.KnownAs))
                .ForMember(dest => dest.NumberOfLikes, opt => opt.MapFrom(src => src.RecipeLikes.Count));
            CreateMap<Recipe, ViewRecipeAdminDTO>()
                .ForMember(dest => dest.RecipeStatusName, opt => opt.MapFrom(src => src.RecipeStatus.Name))
                .ForMember(dest => dest.RecipeComplexityName, opt => opt.MapFrom(src => src.RecipeComplexity.Name))
                .ForMember(dest => dest.RecipeImageUrl, opt => opt.MapFrom(src => src.RecipeImage.Url))
                .ForMember(dest => dest.UserKnownAs, opt => opt.MapFrom(src => src.AppUser.KnownAs));
            CreateMap<Ingredient, IngredientDTO>()
                .ReverseMap();
            CreateMap<RecipeIngredient, RecipeIngredientDTO>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(dest => dest.IngredientServing, opt => opt.MapFrom(src => src.IngredientServing));

            CreateMap<Recipe, RecipeLikeToShowDTO>()
                .ForMember(dest => dest.RecipeStatusName, opt => opt.MapFrom(src => src.RecipeStatus.Name))
                .ForMember(dest => dest.RecipeImageUrl, opt => opt.MapFrom(src => src.RecipeImage.Url))
                .ForMember(dest => dest.UserKnownAs, opt => opt.MapFrom(src => src.AppUser.KnownAs));
            CreateMap<AppUserSubscriptionRecord, SubscriptionRecordDTO>()
                .ReverseMap();
            CreateMap<RecipeStep, RecipeStepDTO>() .ReverseMap();
            CreateMap<RecipeStepImage, RecipeStepImageDTO>()
                .ReverseMap();
            CreateMap<Recipe, RecipeUpdateDTO>().ReverseMap();
            CreateMap<AddRecipeDTO, Recipe>().ReverseMap();
            CreateMap<AddRecipeIngredientDTO, RecipeIngredient>().ReverseMap();
            CreateMap<AddRecipeStepDTO, RecipeStep>().ReverseMap();
            CreateMap<AddRecipeCategoryDTO, RecipeCategory>().ReverseMap();
            CreateMap<AddRecipeImageDTO, RecipeImage>().ReverseMap();
            CreateMap<UserPhoto, UserPhotoDTO>()
                .ReverseMap();
            CreateMap<Transaction, TransactionDTO>()
                .ReverseMap();
        }
    }
}
