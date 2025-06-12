using ValuBakery.Data.Entities.Common;
using ValuBakery.Data.Enums;

namespace ValuBakery.Data.Entities
{
    public class Order : AuditableBaseEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public string Description { get; set; }

        public decimal CostPrice { get; set; }

        public decimal SalePrice { get; set; }

        public decimal Profit { get; set; }

        public Customer Customer { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public StatusEnum Status { get; set; }
    }
}
