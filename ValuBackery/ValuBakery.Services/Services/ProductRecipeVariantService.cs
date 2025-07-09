using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Application.Services
{
    public class ProductRecipeVariantService : IProductRecipeVariantService
    {
        private readonly IProductRecipeVariantDao _dao;
        private readonly IRecipeVariantDao _recipeVariantDao;

        public ProductRecipeVariantService(IProductRecipeVariantDao dao, IRecipeVariantDao recipeVariantDao)
        {
            _dao = dao;
            _recipeVariantDao = recipeVariantDao;
        }

        public async Task<ProductRecipeVariantDto?> GetByIdAsync(int id)
        {
            var productRecipe = await _dao.GetByIdAsync(id);

            if(productRecipe != null)
            {
                productRecipe.RecipeVariant = await _recipeVariantDao.GetByIdAsync(productRecipe.RecipeVariant.Id); 
            }

            return productRecipe;
        }

        public async Task<int> AddAsync(ProductRecipeVariantDto dto)
        {
            var entity = await _dao.GetByProductIdAndRecipeVariantIdAsync(dto.ProductId, dto.RecipeVariantId);

            if (entity != null)
            {
                dto.Id = entity.Id;
                dto.IsDeleted = false;

                await _dao.UpdateAsync(dto);
                return entity.Id;
            }
            else
            {
                return await _dao.AddAsync(dto);
            }
        }

        public async Task<List<ProductRecipeVariantDto>> GetByProductIdAsync(int id)
        {
            return await _dao.GetByProductIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dao.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(ProductRecipeVariantDto dto)
        {
            return await _dao.UpdateAsync(dto);
        }
    }
}
