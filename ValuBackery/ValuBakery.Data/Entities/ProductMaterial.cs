using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class ProductMaterial : AuditableBaseEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int MaterialId { get; set; }

        public Material Material { get; set; }

        public decimal Quantity { get; set; }  // e.g. 1 box, 0.5 ribbon
    }
}
