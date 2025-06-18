using ValuBakery.Data.DTOs;

namespace ValuBakery.Percistence.Percistence.Interfaces
{
    public interface IRecipeDao
    {
        Task<RecipeDto?> GetByIdAsync(int id);
        Task<List<RecipeDto>> GetAllAsync();
        Task<int> AddAsync(RecipeDto dto);
        Task<bool> UpdateAsync(RecipeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
