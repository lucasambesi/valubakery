using System.Security.Claims;
using ValuBakery.Data.Entities.Authorization;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IClaimsService
    {
        Claims Claims { get; }

        ClaimsIdentity? ClaimsIdentity { get; set; }

        int GetUserId();
    }
}
