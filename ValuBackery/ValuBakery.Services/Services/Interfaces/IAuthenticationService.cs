using ValuBakery.Data.DTOs.Authorization;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<TokenDto?> LoginProccess(LoginDto login);
    }
}
