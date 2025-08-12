using ValuBakery.Data.Entities.Common;
using ValuBakery.Data.Enums;

namespace ValuBakery.Data.Entities
{
    public class Order : AuditableBaseEntity
    {
        public int Id { get; set; }

        public DateTime DeliveryDate { get; set; }

        //public int CustomerId { get; set; }

        public string Reference { get; set; }

        //public decimal CostPrice { get; set; }

        //public decimal SalePrice { get; set; }

        //public Customer Customer { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public StatusEnum Status { get; set; }
    }
}
