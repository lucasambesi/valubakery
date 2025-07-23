using System.Security.Claims;
using ValuBakery.Data.Entities.Authorization;
using ValuBakery.Data.Helpers;

namespace ValuBakery.Application.Services.Authorization
{
    public class SystemAuthorization
    {
        public static dynamic GetClaims(ClaimsIdentity identity)
        {
            var userName = identity.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var userId = identity.Claims?
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value.ToInt();

            return new Claims()
            {
                UserName = userName,
                UserId = userId
            };
        }
    }
}
