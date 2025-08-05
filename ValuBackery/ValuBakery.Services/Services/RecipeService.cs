using Microsoft.EntityFrameworkCore;
using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeDao _dao;

        public RecipeService(IRecipeDao dao)
        {
            _dao = dao;
        }

        public async Task<int> AddAsync(RecipeDto dto)
        {
            return await _dao.AddAsync(dto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dao.DeleteAsync(id);
        }

        public async Task<List<RecipeDto>> GetAllAsync()
        {
            return await _dao.GetAllAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _dao.GetCountAsync();
        }

        public async Task<RecipeDto?> GetByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(RecipeDto dto)
        {
            return await _dao.UpdateAsync(dto);
        }
    }
}
