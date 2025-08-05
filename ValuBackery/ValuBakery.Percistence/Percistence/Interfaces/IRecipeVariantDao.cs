using ValuBakery.Data.DTOs;

namespace ValuBakery.Percistence.Percistence
{
    public interface IRecipeVariantDao
    {
        Task<RecipeVariantDto?> GetByIdAsync(int id);
        Task<List<RecipeVariantDto>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<List<RecipeVariantDto>> GetAllExpandedAsync();
        Task<int> AddAsync(RecipeVariantDto dto);
        Task<bool> UpdateAsync(RecipeVariantDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
