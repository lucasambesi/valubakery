using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Percistence.Contexts;
using ValuBakery.Percistence.Percistence.Interfaces;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace ValuBakery.Percistence.Percistence
{
    public class ProductRecipeVariantDao : IProductRecipeVariantDao
    {
        private readonly BaseContext _context;
        private readonly IMapper _mapper;

        public ProductRecipeVariantDao(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(ProductRecipeVariantDto dto)
        {
            var entity = _mapper.Map<ProductRecipeVariant>(dto);
            _context.ProductRecipeVariant.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<ProductRecipeVariantDto> GetByIdAsync(int id)
        {
            var entity = await _context.ProductRecipeVariant
                .Include(rc => rc.RecipeVariant)
                    .ThenInclude(rc => rc.Recipe)
                .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<ProductRecipeVariantDto>(entity);
        }

        public async Task<List<ProductRecipeVariantDto>> GetByProductIdAsync(int productId)
        {
            var items = await _context.ProductRecipeVariant
                .Include(rc => rc.RecipeVariant)
                .ThenInclude(rc => rc.Recipe)
                .Where(rc => rc.ProductId == productId)
                .ToListAsync();

            return _mapper.Map<List<ProductRecipeVariantDto>>(items);
        }

        public async Task<ProductRecipeVariantDto> GetByProductIdAndRecipeVariantIdAsync(int productId, int recipeVariantId)
        {
            var item = await _context.ProductRecipeVariant
                .Where(rc => rc.ProductId == productId && recipeVariantId == rc.RecipeVariantId)
                .FirstOrDefaultAsync();

            return _mapper.Map<ProductRecipeVariantDto>(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ProductRecipeVariant
                            .AsTracking()
                            .FirstOrDefaultAsync(rc => rc.Id == id);

            if (entity is null) return false;

            entity.IsDeleted = true;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(ProductRecipeVariantDto dto)
        {
            var entity = await _context.ProductRecipeVariant.FindAsync(dto.Id);
            if (entity is null) return false;

            entity.Quantity= dto.Quantity;
            entity.IsDeleted = dto.IsDeleted;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
