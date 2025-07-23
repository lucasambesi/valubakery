using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class User : AuditableBaseEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string? Email { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public bool IsDeleted { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
