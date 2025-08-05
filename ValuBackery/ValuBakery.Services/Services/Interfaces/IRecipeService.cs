using ValuBakery.Data.DTOs;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<RecipeDto?> GetByIdAsync(int id);
        Task<List<RecipeDto>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<int> AddAsync(RecipeDto dto);
        Task<bool> UpdateAsync(RecipeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
