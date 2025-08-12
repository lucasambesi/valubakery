using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class OrderItem : AuditableBaseEntity
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitCost { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
