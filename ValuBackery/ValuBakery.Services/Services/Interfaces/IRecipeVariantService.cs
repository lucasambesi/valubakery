using ValuBakery.Data.DTOs;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IRecipeVariantService
    {
        Task<RecipeVariantDto?> GetByIdAsync(int id);
        Task<List<RecipeVariantDto>> GetAllAsync();
        Task<List<RecipeVariantDto>> GetAllExpandedAsync();
        Task<int> AddAsync(RecipeVariantDto dto);
        Task<bool> UpdateAsync(RecipeVariantDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
