using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Percistence.Contexts;

namespace ValuBakery.Percistence.Percistence
{
    public class RecipeDao : IRecipeDao
    {
        private readonly BaseContext _dbContext;
        private readonly IMapper _mapper;

        public RecipeDao(BaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<RecipeDto?> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Recipes
                .Where(r => !r.IsDeleted)
                .Include(r => r.Ingredients)
                .ThenInclude(x => x.Ingredient)
                .Include(r => r.Components)
                .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<RecipeDto>(entity);
        }

        public async Task<List<RecipeDto>> GetAllAsync()
        {
            var entities = await _dbContext.Recipes
                .Where(r => !r.IsDeleted)
                .ToListAsync();

            return _mapper.Map<List<RecipeDto>>(entities);
        }

        public async Task<int> AddAsync(RecipeDto dto)
        {
            var entity = _mapper.Map<Recipe>(dto);
            _dbContext.Recipes.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(RecipeDto dto)
        {
            var entity = await _dbContext.Recipes.FindAsync(dto.Id);
            if (entity is null || entity.IsDeleted) return false;

            _mapper.Map(dto, entity);

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.Recipes.FindAsync(id);
            if (entity is null || entity.IsDeleted) return false;

            entity.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

}
