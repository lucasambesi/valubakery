using AutoMapper;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;

namespace ValuBakery.Application.Mappings
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            #region Entities
            CreateMap<UserDto, User>();
            CreateMap<IngredientDto, Ingredient>();
            CreateMap<RecipeDto, Recipe>();
            CreateMap<RecipeComponentDto, RecipeComponent>();
            CreateMap<RecipeIngredientDto, RecipeIngredient>();
            #endregion

            #region Dtos
            CreateMap<User, UserDto>();
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeComponent, RecipeComponentDto>();
            CreateMap<RecipeIngredient, RecipeIngredientDto>();
            #endregion
        }
    }
}
