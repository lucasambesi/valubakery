using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Entities;
using ValuBakery.Data.Helpers;
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

        public async Task<UserDto?> GetUserByCredentialsAsync(string userName, string password)
        {
            password = EncryptPasswordAsync(password);

            return await _UserDao.GetUserByCredentialsAsync(userName, password);
        }

        public string EncryptPasswordAsync(string password)
        {
            return Encrypt.GetSHA256(password);
        }

        public async Task<bool> UpdateByLoginAsync(UserDto userDto)
        {
            return await _UserDao.UpdateAsync(userDto);
        }

        public async Task<bool> UpdateAsync(UserDto userDto)
        {
            return await _UserDao.UpdateAsync(userDto);
        }
    }
}
