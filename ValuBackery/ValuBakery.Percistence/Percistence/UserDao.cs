using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Contexts;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Percistence.Percistence
{
    public class UserDao : IUserDao
    {
        private readonly BaseContext _dbContext;

        private readonly IMapper _mapper;

        public UserDao(
            BaseContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(l => l.Id == id);

            var result = _mapper.Map<UserDto>(user);

            return result;
        }

        public async Task<UserDto?> GetUserByCredentialsAsync(string userName, string password)
        {
            var query = await _dbContext.Users.FirstOrDefaultAsync(
                l => l.UserName == userName &&
                l.Password == password);

            var result = _mapper.Map<UserDto>(query);

            return result;
        }

        public async Task<bool> UpdateAsync(UserDto dto)
        {
            var entity = await _dbContext.Users.FindAsync(dto.Id);
            if (entity is null) return false;

            _mapper.Map(dto, entity);

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
