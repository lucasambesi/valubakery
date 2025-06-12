namespace ValuBakery.Data.Entities
{
    public class EventOrder
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
