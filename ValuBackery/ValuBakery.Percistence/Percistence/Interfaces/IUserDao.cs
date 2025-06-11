using ValuBakery.Data.DTOs;

namespace ValuBakery.Percistence.Percistence.Interfaces
{
    public interface IUserDao
    {
        Task<UserDto?> GetByIdAsync(int id);
    }
}
