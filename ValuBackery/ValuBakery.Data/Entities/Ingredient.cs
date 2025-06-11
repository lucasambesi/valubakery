using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuBakery.Data.Entities.Common;
using ValuBakery.Data.Enums;

namespace ValuBakery.Data.Entities
{
    public class Ingredient : AuditableBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UnitEnum Unit { get; set; }

        public decimal Cost { get; set; }
    }

}
