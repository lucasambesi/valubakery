using System.Security.Claims;
using ValuBakery.Application.Services.Authorization;
using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.Entities.Authorization;

namespace ValuBakery.Application.Services
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsIdentity? ClaimsIdentity { get; set; }

        public Claims? Claims => SystemAuthorization.GetClaims(ClaimsIdentity);

        public ClaimsService() { }

        public int GetUserId()
        {
            return Claims != null && Claims.UserId != null ? (int)Claims.UserId : 0;
        }
    }
}
