using ValuBakery.Data.DTOs;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IProductMaterialService
    {
        Task<ProductMaterialDto?> GetByIdAsync(int id);
        Task<int> AddAsync(ProductMaterialDto dto);
        Task<List<ProductMaterialDto>> GetByProductIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(ProductMaterialDto dto);
    }
}
