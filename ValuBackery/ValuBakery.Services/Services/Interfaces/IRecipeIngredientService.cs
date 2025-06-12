using ValuBakery.Data.DTOs;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IRecipeIngredientService
    {
        Task<int> AddAsync(RecipeIngredientDto dto);

        Task<RecipeIngredientDto?> GetByIdAsync(int id);

        Task<List<RecipeIngredientDto>> GetByRecipeIdAsync(int recipeId);

        Task<bool> UpdateAsync(RecipeIngredientDto dto);

        Task<bool> DeleteAsync(int id);
    }

}
