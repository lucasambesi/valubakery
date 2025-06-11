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
    }
}
