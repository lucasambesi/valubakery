using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Percistence.Contexts;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Percistence.Percistence
{
    public class ProductDao : IProductDao
    {
        private readonly BaseContext _dbContext;
        private readonly IMapper _mapper;

        public ProductDao(BaseContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Product
                .Include(rc => rc.ProductRecipeVariants.Where(x => !x.IsDeleted))
                    .ThenInclude(rc => rc.RecipeVariant)
                .Include(productMaterials => productMaterials.ProductMaterials.Where(x => !x.IsDeleted))
                        .ThenInclude(material => material.Material)

                .Where(i => !i.IsDeleted)
                .FirstOrDefaultAsync(i => i.Id == id);

            return _mapper.Map<ProductDto>(entity);
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var entities = await _dbContext.Product
                .Where(i => !i.IsDeleted)
                .OrderBy(i => i.Name)
                .ToListAsync();

            return _mapper.Map<List<ProductDto>>(entities);
        }

        public async Task<int> AddAsync(ProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            _dbContext.Product.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(ProductDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var entity = await _dbContext.Product.FindAsync(dto.Id);
            if (entity is null)
                return false;

            _mapper.Map(dto, entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Product.FindAsync(id);
            if (entity is null) return;

            entity.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
