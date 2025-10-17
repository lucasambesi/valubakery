using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Percistence.Contexts;
using ValuBakery.Percistence.Percistence.Interfaces;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

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

        public async Task<int> AddAsync(RecipeDto dto)
        {
            var entity = _mapper.Map<Recipe>(dto);
            _dbContext.Recipe.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.Recipe.FindAsync(id);
            if (entity is null || entity.IsDeleted) return false;

            entity.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Recipe
                .Where(r => !r.IsDeleted)
                .CountAsync();
        }

        public async Task<List<RecipeDto>> GetAllAsync()
        {
            var entities = await _dbContext.Recipe
                .Include(x => x.Variants)
                .Where(r => !r.IsDeleted)
                .ToListAsync();

            return _mapper.Map<List<RecipeDto>>(entities);
        }

        public async Task<RecipeDto?> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Recipe
                .Include(rc => rc.Variants)
                .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<RecipeDto>(entity);
        }

        public async Task<bool> UpdateAsync(RecipeDto dto)
        {
            var entity = await _dbContext.Recipe.FindAsync(dto.Id);
            if (entity is null) return false;

            //_mapper.Map(dto, entity);

            entity.Name= dto.Name;
            entity.Description= dto.Description;
            entity.IsDeleted = dto.IsDeleted;

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
