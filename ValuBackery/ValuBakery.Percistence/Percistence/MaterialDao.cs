using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Percistence.Contexts;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Percistence.Percistence
{
    public class MaterialDao : IMaterialDao
    {
        private readonly BaseContext _dbContext;
        private readonly IMapper _mapper;

        public MaterialDao(BaseContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public async Task<MaterialDto?> GetByIdAsync(int id)
        {
            var entity = await _dbContext.Materials
                .Where(i => !i.IsDeleted)
                .FirstOrDefaultAsync(i => i.Id == id);

            return _mapper.Map<MaterialDto>(entity);
        }

        public async Task<List<MaterialDto>> GetAllAsync()
        {
            var entities = await _dbContext.Materials
                .Where(i => !i.IsDeleted)
                .OrderBy(i => i.Name)
                .ToListAsync();

            return _mapper.Map<List<MaterialDto>>(entities);
        }

        public async Task<int> AddAsync(MaterialDto dto)
        {
            var entity = _mapper.Map<Material>(dto);
            _dbContext.Materials.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateAsync(MaterialDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var entity = await _dbContext.Materials.FindAsync(dto.Id);
            if (entity is null)
                return false;

            _mapper.Map(dto, entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Materials.FindAsync(id);
            if (entity is null) return;

            entity.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
