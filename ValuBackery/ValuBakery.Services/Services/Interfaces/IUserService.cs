using ValuBakery.Data.DTOs;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(int id);

        Task<UserDto?> GetUserByCredentialsAsync(string userName, string password);

        Task<bool> UpdateByLoginAsync(UserDto userDto);

        Task<bool> UpdateAsync(UserDto userDto);
    }
}
