using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence;

namespace ValuBakery.Application.Services
{
    public class RecipeVariantService : IRecipeVariantService
    {
        private readonly IRecipeVariantDao _dao;

        public RecipeVariantService(IRecipeVariantDao recipeDao)
        {
            _dao = recipeDao;
        }

        public async Task<RecipeVariantDto?> GetByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<List<RecipeVariantDto>> GetAllAsync()
        {
            return await _dao.GetAllAsync();
        }
        public async Task<int> GetCountAsync()
        {
            return await _dao.GetCountAsync();
        }
        public async Task<List<RecipeVariantDto>> GetAllExpandedAsync()
        {
            return await _dao.GetAllExpandedAsync();
        }

        public async Task<int> AddAsync(RecipeVariantDto dto)
        {
            return await _dao.AddAsync(dto);
        }

        public async Task<bool> UpdateAsync(RecipeVariantDto dto)
        {
            return await _dao.UpdateAsync(dto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dao.DeleteAsync(id);
        }
    }

}
