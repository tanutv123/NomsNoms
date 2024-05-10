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
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.First().AppRole.Name))
                .ReverseMap();
            CreateMap<AppUser, UserProfileDTO>()
                .ReverseMap();
            CreateMap<Recipe, RecipeDTO>()
                .ForMember(dest => dest.RecipeStatusName, opt => opt.MapFrom(src => src.RecipeStatus.Name))
                .ForMember(dest => dest.RecipeComplexityName, opt => opt.MapFrom(src => src.RecipeComplexity.Name))
                .ForMember(dest => dest.RecipeImageUrl, opt => opt.MapFrom(src => src.RecipeImage.Url))
                .ForMember(dest => dest.UserKnownAs, opt => opt.MapFrom(src => src.AppUser.KnownAs));
            CreateMap<Ingredient, IngredientDTO>()
                .ReverseMap();
            CreateMap<RecipeIngredient, RecipeIngredientDTO>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name));
        }
    }
}