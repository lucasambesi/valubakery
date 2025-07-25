﻿using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using ValuBakery.Application.Services;
using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Percistence.Percistence;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Bootstrap.Providers.Cofigurations
{
    [ExcludeFromCodeCoverage]
    public static class AddServices
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIngredientService, IngredientService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IRecipeVariantService, RecipeVariantService>();
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IRecipeIngredientService, RecipeIngredientService>();
            services.AddScoped<IRecipeComponentService, RecipeComponentService>();
            services.AddScoped<IProductMaterialService, ProductMaterialService>();
            services.AddScoped<IProductRecipeVariantService, ProductRecipeVariantService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IClaimsService, ClaimsService>();

            // Dao
            services.AddScoped<IUserDao, UserDao>();
            services.AddScoped<IIngredientDao, IngredientDao>();
            services.AddScoped<IMaterialDao, MaterialDao>();
            services.AddScoped<IRecipeDao, RecipeDao>();
            services.AddScoped<IRecipeVariantDao, RecipeVariantDao>();
            services.AddScoped<IRecipeIngredientDao, RecipeIngredientDao>();
            services.AddScoped<IRecipeComponentDao, RecipeComponentDao>();
            services.AddScoped<IProductMaterialDao, ProductMaterialDao>();
            services.AddScoped<IProductRecipeVariantDao, ProductRecipeVariantDao>();
            services.AddScoped<IProductDao, ProductDao>();

            return services;
        }
    }
}
