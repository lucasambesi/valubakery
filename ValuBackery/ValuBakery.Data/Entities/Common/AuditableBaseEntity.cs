namespace ValuBakery.Data.Entities.Common
{
    public abstract class AuditableBaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
