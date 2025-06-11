using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDao _UserDao;

        public UserService(
            IUserDao UserDao)
        {
            _UserDao = UserDao;
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            return await _UserDao.GetByIdAsync(id);
        }
    }
}
