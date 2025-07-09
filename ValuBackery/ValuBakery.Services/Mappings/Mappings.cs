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
            CreateMap<MaterialDto, Material>();
            CreateMap<RecipeDto, Recipe>();
            CreateMap<RecipeVariantDto, RecipeVariant>();

            CreateMap<RecipeComponentDto, RecipeComponent>();
            CreateMap<RecipeIngredientDto, RecipeIngredient>();

            CreateMap<ProductDto, Product>();
            CreateMap<ProductMaterialDto, ProductMaterial>();
            CreateMap<ProductRecipeVariantDto, ProductRecipeVariant>();
            #endregion

            #region Dtos
            CreateMap<User, UserDto>();
            CreateMap<Ingredient, IngredientDto>();
            CreateMap<Material, MaterialDto>();
            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeVariant, RecipeVariantDto>();
            CreateMap<RecipeComponent, RecipeComponentDto>();
            CreateMap<RecipeIngredient, RecipeIngredientDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductMaterial, ProductMaterialDto>();
            CreateMap<ProductRecipeVariant, ProductRecipeVariantDto>();
            #endregion
        }
    }
}
