using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence;

namespace ValuBakery.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeDao _recipeDao;

        public RecipeService(IRecipeDao recipeDao)
        {
            _recipeDao = recipeDao;
        }

        public async Task<RecipeDto?> GetByIdAsync(int id)
        {
            return await _recipeDao.GetByIdAsync(id);
        }

        public async Task<List<RecipeDto>> GetAllAsync()
        {
            return await _recipeDao.GetAllAsync();
        }

        public async Task<int> AddAsync(RecipeDto dto)
        {
            return await _recipeDao.AddAsync(dto);
        }

        public async Task<bool> UpdateAsync(RecipeDto dto)
        {
            return await _recipeDao.UpdateAsync(dto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _recipeDao.DeleteAsync(id);
        }
    }

}
