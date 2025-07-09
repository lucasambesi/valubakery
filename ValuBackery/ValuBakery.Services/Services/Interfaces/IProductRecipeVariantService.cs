using ValuBakery.Data.DTOs;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IProductRecipeVariantService
    {
        Task<ProductRecipeVariantDto?> GetByIdAsync(int id);
        Task<int> AddAsync(ProductRecipeVariantDto dto);
        Task<List<ProductRecipeVariantDto>> GetByProductIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(ProductRecipeVariantDto dto);
    }
}
