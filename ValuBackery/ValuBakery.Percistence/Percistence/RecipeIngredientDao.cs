using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Percistence.Contexts;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Percistence.Percistence
{
    public class RecipeIngredientDao : IRecipeIngredientDao
    {
        private readonly BaseContext _dbContext;
        private readonly IMapper _mapper;

        public RecipeIngredientDao(BaseContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(RecipeIngredientDto dto)
        {
            var entity = _mapper.Map<RecipeIngredient>(dto);
            _dbContext.RecipeIngredients.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<RecipeIngredientDto> GetByIdAsync(int id)
        {
            var entity = await _dbContext.RecipeIngredients
                .Include(r => r.Ingredient)
                .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<RecipeIngredientDto>(entity);
        }


        public async Task<List<RecipeIngredientDto>> GetByRecipeIdAsync(int recipeId)
        {
            var items = await _dbContext.RecipeIngredients
                .Include(x => x.Ingredient)
                .Where(r => r.RecipeVariantId == recipeId)
                .ToListAsync();

            return _mapper.Map<List<RecipeIngredientDto>>(items);
        }

        public async Task<bool> UpdateAsync(RecipeIngredientDto dto)
        {
            var entity = await _dbContext.RecipeIngredients.FindAsync(dto.Id);
            if (entity is null) return false;

            _mapper.Map(dto, entity);

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.RecipeIngredients.FindAsync(id);
            if (entity == null) return false;

            _dbContext.RecipeIngredients.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
