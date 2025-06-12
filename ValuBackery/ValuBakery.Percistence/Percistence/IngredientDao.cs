using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Percistence.Contexts;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Percistence.Percistence
{
    public class IngredientDao : IIngredientDao
    {
        private readonly BaseContext _dbContext;
        private readonly IMapper _mapper;

        public IngredientDao(BaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IngredientDto?> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Ingredients
                .Where(i => !i.IsDeleted)
                .FirstOrDefaultAsync(i => i.Id == id);

            return _mapper.Map<IngredientDto>(entity);
        }

        public async Task<List<IngredientDto>> GetAllAsync()
        {
            var entities = await _dbContext.Ingredients
                .Where(i => !i.IsDeleted)
                .OrderBy(i => i.Name)
                .ToListAsync();

            return _mapper.Map<List<IngredientDto>>(entities);
        }

        public async Task<int> AddAsync(IngredientDto dto)
        {
            var entity = _mapper.Map<Ingredient>(dto);
            _dbContext.Ingredients.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(IngredientDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var entity = await _dbContext.Ingredients.FindAsync(dto.Id);
            if (entity is null)
                return false;

            _mapper.Map(dto, entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }


        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Ingredients.FindAsync(id);
            if (entity is null) return;

            entity.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
