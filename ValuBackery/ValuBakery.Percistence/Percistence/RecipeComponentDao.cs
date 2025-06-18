using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Percistence.Contexts;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Percistence.Percistence
{
    public class RecipeComponentDao : IRecipeComponentDao
    {
        private readonly BaseContext _context;
        private readonly IMapper _mapper;

        public RecipeComponentDao(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(RecipeComponentDto dto)
        {
            var entity = _mapper.Map<RecipeComponent>(dto);
            _context.RecipeComponents.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<RecipeComponentDto> GetByIdAsync(int id)
        {
            var entity = await _context.RecipeComponents
                .Include(rc => rc.ChildRecipeVariant)
                .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<RecipeComponentDto>(entity);
        }

        public async Task<List<RecipeComponentDto>> GetByRecipeIdAsync(int parentRecipeId)
        {
            var items = await _context.RecipeComponents
                .Include(rc => rc.ChildRecipeVariant)
                    .ThenInclude(rc => rc.Recipe)
                .Where(rc => rc.ParentRecipeVariantId == parentRecipeId)
                .ToListAsync();

            return items.Select(rc => new RecipeComponentDto
            {
                ParentRecipeVariantId = rc.ParentRecipeVariantId,
                ChildRecipeVariantId = rc.ChildRecipeVariantId,
                Quantity = rc.Quantity,
                ChildRecipeName = rc.ChildRecipeVariant.Recipe.Name + " " + rc.ChildRecipeVariant.Name
            }).ToList();
        }

        public async Task<bool> DeleteAsync(int parentRecipeId, int childRecipeId)
        {
            var trackedEntity = await _context.RecipeComponents
                .FirstOrDefaultAsync(rc => rc.ParentRecipeVariantId == parentRecipeId && rc.ChildRecipeVariantId == childRecipeId);

            if (trackedEntity is null) return false;

            _context.Entry(trackedEntity).State = EntityState.Detached;
            _context.RecipeComponents.Attach(trackedEntity);
            _context.RecipeComponents.Remove(trackedEntity);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(RecipeComponentDto dto)
        {
            var entity = await _context.RecipeComponents.FindAsync(dto.Id);
            if (entity is null) return false;

            _mapper.Map(dto, entity);

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
