using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Percistence.Contexts;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace ValuBakery.Percistence.Percistence
{
    public class RecipeVariantDao : IRecipeVariantDao
    {
        private readonly BaseContext _dbContext;
        private readonly IMapper _mapper;

        public RecipeVariantDao(BaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }       

        public async Task<RecipeVariantDto?> GetByIdAsync(int id)
        {
            var recipe = await _dbContext.RecipeVariant
                .Include(r => r.Recipe)
                .Include(r => r.Ingredients.Where(x => !x.IsDeleted))
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Components.Where(x => !x.IsDeleted))
                    .ThenInclude(rc => rc.ChildRecipeVariant)
                .Include(r => r.UsedIn)
                    .ThenInclude(uc => uc.ParentRecipeVariant)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            if (recipe == null)
                return null;

            var visited = new HashSet<int>();
            return await BuildRecipeDtoRecursively(recipe, visited);
        }

        private async Task<RecipeVariantDto> BuildRecipeDtoRecursively(RecipeVariant recipe, HashSet<int> visited)
        {
            if (visited.Contains(recipe.Id))
                return null;

            visited.Add(recipe.Id);

            // Mapear receta principal
            var dto = _mapper.Map<RecipeVariantDto>(recipe);

            // Mapear ingredientes directamente
            dto.Ingredients = _mapper.Map<List<RecipeIngredientDto>>(recipe.Ingredients);

            // Mapear UsedIn
            dto.UsedIn = _mapper.Map<List<RecipeComponentDto>>(recipe.UsedIn);

            // Mapear componentes recursivamente
            var mappedComponents = new List<RecipeComponentDto>();
            foreach (var comp in recipe.Components)
            {
                var compDto = _mapper.Map<RecipeComponentDto>(comp);

                if (comp.ChildRecipeVariant != null && !visited.Contains(comp.ChildRecipeVariant.Id))
                {
                    // Cargar receta hija completa si no está completamente cargada
                    var childRecipe = await _dbContext.RecipeVariant
                        .Include(r => r.Recipe)
                        .Include(r => r.Ingredients.Where(x => !x.IsDeleted))
                            .ThenInclude(ri => ri.Ingredient)
                        .Include(r => r.Components.Where(x => !x.IsDeleted))
                            .ThenInclude(c => c.ChildRecipeVariant)
                        .FirstOrDefaultAsync(r => r.Id == comp.ChildRecipeVariant.Id && !r.IsDeleted);

                    if (childRecipe != null)
                        compDto.ChildRecipeVariant = await BuildRecipeDtoRecursively(childRecipe, visited);
                }

                mappedComponents.Add(compDto);
            }

            dto.Components = mappedComponents;
            return dto;
        }

        public async Task<List<RecipeVariantDto>> GetAllAsync()
        {
            var entities = await _dbContext.RecipeVariant
                .Include(r => r.Recipe)
                .Where(r => !r.IsDeleted)
                .ToListAsync();

            return _mapper.Map<List<RecipeVariantDto>>(entities);
        }

        public async Task<List<RecipeVariantDto>> GetAllExpandedAsync()
        {
            var entities = await _dbContext.RecipeVariant
                .Include(r => r.Recipe)
                .Include(r => r.Components.Where(x => !x.IsDeleted))
                .Include(r => r.UsedIn)
                .Where(r => !r.IsDeleted)
                .ToListAsync();

            return _mapper.Map<List<RecipeVariantDto>>(entities);
        }

        public async Task<int> AddAsync(RecipeVariantDto dto)
        {
            var entity = _mapper.Map<RecipeVariant>(dto);
            _dbContext.RecipeVariant.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(RecipeVariantDto dto)
        {
            var entity = await _dbContext.RecipeVariant.FindAsync(dto.Id);
            if (entity is null || entity.IsDeleted) return false;

            _mapper.Map(dto, entity);

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _dbContext.RecipeVariant.FindAsync(id);
            if (entity is null || entity.IsDeleted) return false;

            entity.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

}
