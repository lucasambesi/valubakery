using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDao _productDao;
        private readonly IRecipeVariantDao _recipeVariantDao;

        public ProductService(IProductDao productDao, IRecipeVariantDao recipeVariantDao)
        {
            _productDao = productDao;
            _recipeVariantDao = recipeVariantDao;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productDao.GetByIdAsync(id);

            if(product == null) { return null; }

            foreach (var variant in product.ProductRecipeVariants)
            {
                variant.RecipeVariant = await _recipeVariantDao.GetByIdAsync(variant.RecipeVariantId);
            }

            return product;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            return await _productDao.GetAllAsync();
        }

        public async Task<int> AddAsync(ProductDto dto)
        {
            return await _productDao.AddAsync(dto);
        }

        public async Task<bool> UpdateAsync(ProductDto dto)
        {
            return await _productDao.UpdateAsync(dto);
        }

        public async Task DeleteAsync(int id)
        {
            await _productDao.DeleteAsync(id);
        }
    }
}
