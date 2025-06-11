using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class Expense : AuditableBaseEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
    }
}
