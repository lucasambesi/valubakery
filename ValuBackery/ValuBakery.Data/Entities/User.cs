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
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
