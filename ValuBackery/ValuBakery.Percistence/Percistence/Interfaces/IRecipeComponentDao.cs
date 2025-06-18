using ValuBakery.Data.DTOs;

namespace ValuBakery.Percistence.Percistence.Interfaces
{
    public interface IRecipeComponentDao
    {
        Task<RecipeComponentDto?> GetByIdAsync(int id);
        Task<int> AddAsync(RecipeComponentDto dto);
        Task<List<RecipeComponentDto>> GetByRecipeIdAsync(int parentRecipeId);
        Task<bool> DeleteAsync(int parentRecipeId, int childRecipeId);
        Task<bool> UpdateAsync(RecipeComponentDto dto);
    }
}
