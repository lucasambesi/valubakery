using ValuBakery.Data.DTOs;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto?> GetByIdAsync(int id);
        Task<List<ProductDto>> GetAllAsync();
        Task<int> AddAsync(ProductDto dto);
        Task<bool> UpdateAsync(ProductDto dto);
        Task DeleteAsync(int id);
    }
}
