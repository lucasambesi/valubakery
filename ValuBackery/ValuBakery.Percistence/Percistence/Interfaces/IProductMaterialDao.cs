using ValuBakery.Data.DTOs;

namespace ValuBakery.Percistence.Percistence.Interfaces
{
    public interface IProductMaterialDao
    {
        Task<ProductMaterialDto?> GetByIdAsync(int id);
        Task<ProductMaterialDto> GetByProductIdAndMaterialIdAsync(int productId, int materialId);
        Task<int> AddAsync(ProductMaterialDto dto);
        Task<List<ProductMaterialDto>> GetByProductIdAsync(int productId);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(ProductMaterialDto dto);
    }
}
