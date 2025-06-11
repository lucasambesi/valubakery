using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.Entities
{
    public class Customer : AuditableBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Reference { get; set; }
    }

}
