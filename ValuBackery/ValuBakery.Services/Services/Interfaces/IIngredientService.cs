using ValuBakery.Data.DTOs;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IIngredientService
    {
        Task<IngredientDto?> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task<List<IngredientDto>> GetAllAsync();
        Task<int> AddAsync(IngredientDto dto);
        Task<bool> UpdateAsync(IngredientDto dto);
        Task DeleteAsync(int id);
    }

}
