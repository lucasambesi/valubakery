using ValuBakery.Data.DTOs;

namespace ValuBakery.Percistence.Percistence.Interfaces
{
    public interface IUserDao
    {
        Task<UserDto?> GetByIdAsync(int id);

        Task<UserDto?> GetUserByCredentialsAsync(string userName, string password);

        Task<bool> UpdateAsync(UserDto dto);
    }
}
