using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;

namespace ValuBakery.Percistence.Percistence.Interfaces
{
    public interface IMaterialDao
    {
        Task<MaterialDto?> GetByIdAsync(int id);
        Task<List<MaterialDto>> GetAllAsync();
        Task<int> AddAsync(MaterialDto dto);
        Task<bool> UpdateAsync(MaterialDto dto);
        Task DeleteAsync(int id);
    }
}
