using ValuBakery.Data.DTOs;

namespace ValuBakery.Percistence.Percistence.Interfaces
{
    public interface IProductRecipeVariantDao
    {
        Task<ProductRecipeVariantDto?> GetByIdAsync(int id);

        Task<ProductRecipeVariantDto> GetByProductIdAndRecipeVariantIdAsync(int productId, int recipeVariantId);

        Task<int> AddAsync(ProductRecipeVariantDto dto);

        Task<List<ProductRecipeVariantDto>> GetByProductIdAsync(int productId);

        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateAsync(ProductRecipeVariantDto dto);
    }
}
