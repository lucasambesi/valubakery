using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Percistence.Contexts;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Percistence.Percistence
{
    public class ProductMaterialDao : IProductMaterialDao
    {
        private readonly BaseContext _context;
        private readonly IMapper _mapper;

        public ProductMaterialDao(BaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(ProductMaterialDto dto)
        {
            var entity = _mapper.Map<ProductMaterial>(dto);

            _context.ProductMaterial.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<ProductMaterialDto> GetByIdAsync(int id)
        {
            var entity = await _context.ProductMaterial
                .Include(rc => rc.Material)
                .FirstOrDefaultAsync(r => r.Id == id);

            return _mapper.Map<ProductMaterialDto>(entity);
        }

        public async Task<List<ProductMaterialDto>> GetByProductIdAsync(int productId)
        {
            var items = await _context.ProductMaterial
                .Include(rc => rc.Material)
                .Where(rc => rc.ProductId == productId)
                .ToListAsync();

            return _mapper.Map<List<ProductMaterialDto>>(items);
        }

        public async Task<ProductMaterialDto> GetByProductIdAndMaterialIdAsync(int productId, int materialId)
        {
            var item = await _context.ProductMaterial
                .Where(rc => rc.ProductId == productId && materialId == rc.MaterialId)
                .FirstOrDefaultAsync();

            return _mapper.Map<ProductMaterialDto>(item);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ProductMaterial
                            .AsTracking()
                            .FirstOrDefaultAsync(rc => rc.Id == id);

            if (entity is null) return false;

            entity.IsDeleted = true;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(ProductMaterialDto dto)
        {
            var entity = await _context.ProductMaterial.FindAsync(dto.Id);
            if (entity is null) return false;

            entity.Quantity = dto.Quantity;
            entity.IsDeleted = dto.IsDeleted;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
