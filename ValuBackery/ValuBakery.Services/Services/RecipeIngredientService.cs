using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Application.Services
{
    public class RecipeIngredientService : IRecipeIngredientService
    {
        private readonly IRecipeIngredientDao _dao;

        public RecipeIngredientService(IRecipeIngredientDao dao)
        {
            _dao = dao;
        }

        public async Task<int> AddAsync(RecipeIngredientDto dto)
        {
            return await _dao.AddAsync(dto);
        }

        public async Task<bool> UpdateAsync(RecipeIngredientDto dto)
        {
            return await _dao.UpdateAsync(dto);
        }

        public async Task<RecipeIngredientDto?> GetByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }


        public async Task<List<RecipeIngredientDto>> GetByRecipeIdAsync(int recipeId)
        {
            return await _dao.GetByRecipeIdAsync(recipeId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dao.DeleteAsync(id);
        }
    }

}
