using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientDao _ingredientDao;

        public IngredientService(IIngredientDao ingredientDao)
        {
            _ingredientDao = ingredientDao;
        }

        public async Task<IngredientDto?> GetByIdAsync(int id)
        {
            return await _ingredientDao.GetByIdAsync(id);
        }

        public async Task<List<IngredientDto>> GetAllAsync()
        {
            return await _ingredientDao.GetAllAsync();
        }

        public async Task<int> AddAsync(IngredientDto dto)
        {
            return await _ingredientDao.AddAsync(dto);
        }

        public async Task<bool> UpdateAsync(IngredientDto dto)
        {
            return await _ingredientDao.UpdateAsync(dto);
        }

        public async Task DeleteAsync(int id)
        {
            await _ingredientDao.DeleteAsync(id);
        }
    }
}
