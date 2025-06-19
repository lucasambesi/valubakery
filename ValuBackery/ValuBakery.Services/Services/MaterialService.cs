using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Application.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialDao _MaterialDao;

        public MaterialService(IMaterialDao MaterialDao)
        {
            _MaterialDao = MaterialDao;
        }

        public async Task<MaterialDto?> GetByIdAsync(int id)
        {
            return await _MaterialDao.GetByIdAsync(id);
        }

        public async Task<List<MaterialDto>> GetAllAsync()
        {
            return await _MaterialDao.GetAllAsync();
        }

        public async Task<int> AddAsync(MaterialDto dto)
        {
            return await _MaterialDao.AddAsync(dto);
        }

        public async Task<bool> UpdateAsync(MaterialDto dto)
        {
            return await _MaterialDao.UpdateAsync(dto);
        }

        public async Task DeleteAsync(int id)
        {
            await _MaterialDao.DeleteAsync(id);
        }
    }
}
