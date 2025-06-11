using ValuBakery.Data.DTOs;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(int id);
    }
}
