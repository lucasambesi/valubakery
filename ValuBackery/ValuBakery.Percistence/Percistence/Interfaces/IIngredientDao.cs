using ValuBakery.Data.DTOs;

namespace ValuBakery.Percistence.Percistence.Interfaces
{
    public interface IIngredientDao
    {
        Task<IngredientDto?> GetByIdAsync(int id);
        Task<List<IngredientDto>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<int> AddAsync(IngredientDto dto);
        Task<bool> UpdateAsync(IngredientDto dto);
        Task DeleteAsync(int id);
    }
}
