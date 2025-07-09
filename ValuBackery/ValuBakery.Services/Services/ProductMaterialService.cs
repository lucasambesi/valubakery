using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Application.Services
{
    public class ProductMaterialService : IProductMaterialService
    {
        private readonly IProductMaterialDao _dao;

        public ProductMaterialService(IProductMaterialDao dao)
        {
            _dao = dao;
        }

        public async Task<ProductMaterialDto?> GetByIdAsync(int id)
        {
            var productMaterial = await _dao.GetByIdAsync(id);

            return productMaterial;
        }

        public async Task<int> AddAsync(ProductMaterialDto dto)
        {
            var entity = await _dao.GetByProductIdAndMaterialIdAsync(dto.ProductId, dto.MaterialId);

            if (entity != null)
            {
                dto.Id = entity.Id;
                dto.IsDeleted = false;

                await _dao.UpdateAsync(dto);
                return entity.Id;
            }
            else
            {
                return await _dao.AddAsync(dto);
            }
        }

        public async Task<List<ProductMaterialDto>> GetByProductIdAsync(int id)
        {
            return await _dao.GetByProductIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dao.DeleteAsync(id);
        }

        public async Task<bool> UpdateAsync(ProductMaterialDto dto)
        {
            return await _dao.UpdateAsync(dto);
        }
    }
}
