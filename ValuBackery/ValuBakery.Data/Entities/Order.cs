using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuBakery.Data.Entities.Common;
using ValuBakery.Data.Enums;

namespace ValuBakery.Data.Entities
{
    public class Order : AuditableBaseEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<OrderItem> Items { get; set; }
        public StatusEnum Status { get; set; }
    }

}
