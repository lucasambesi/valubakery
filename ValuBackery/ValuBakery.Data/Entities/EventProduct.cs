namespace ValuBakery.Data.Entities
{
    public class EventProduct
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int StockAvailable { get; set; }
    }
}
