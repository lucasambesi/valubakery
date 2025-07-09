using ValuBakery.Data.DTOs;

namespace ValuBakery.Percistence.Percistence.Interfaces
{
    public interface IProductDao
    {
        Task<ProductDto?> GetByIdAsync(int id);
        Task<List<ProductDto>> GetAllAsync();
        Task<int> AddAsync(ProductDto dto);
        Task<bool> UpdateAsync(ProductDto dto);
        Task DeleteAsync(int id);
    }
}
