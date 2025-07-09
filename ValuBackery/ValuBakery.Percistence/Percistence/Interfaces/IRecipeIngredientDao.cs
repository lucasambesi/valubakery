using ValuBakery.Data.DTOs;

namespace ValuBakery.Percistence.Percistence.Interfaces
{
    public interface IRecipeIngredientDao
    {
        Task<int> AddAsync(RecipeIngredientDto dto);

        Task<RecipeIngredientDto> GetByIdAsync(int id);

        Task<RecipeIngredientDto> GetByRecipeIdAndIngredienIdAsync(int recipeId, int ingredientId);

        Task<List<RecipeIngredientDto>> GetByRecipeIdAsync(int recipeId);

        Task<bool> UpdateAsync(RecipeIngredientDto dto);

        Task<bool> DeleteAsync(int id);
    }

}
