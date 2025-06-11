using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class CustomOrder : AuditableBaseEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public string Description { get; set; }
        public string Status { get; set; }

        public List<CustomOrderRecipe> UsedRecipes { get; set; }
    }

}
