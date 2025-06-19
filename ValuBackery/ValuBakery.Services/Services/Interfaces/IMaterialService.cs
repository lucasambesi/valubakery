using ValuBakery.Data.DTOs;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IMaterialService
    {
        Task<MaterialDto?> GetByIdAsync(int id);
        Task<List<MaterialDto>> GetAllAsync();
        Task<int> AddAsync(MaterialDto dto);
        Task<bool> UpdateAsync(MaterialDto dto);
        Task DeleteAsync(int id);
    }
}
