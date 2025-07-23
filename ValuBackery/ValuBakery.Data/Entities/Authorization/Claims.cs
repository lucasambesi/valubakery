using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuBakery.Data.Entities.Authorization
{
    public class Claims
    {
        public int? UserId { get; set; }

        public string? UserName { get; set; }
    }
}
