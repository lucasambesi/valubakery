namespace ValuBakery.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public List<EventProduct> EventProducts { get; set; } = new();

        public List<EventOrder> EventOrders { get; set; } = new();
    }
}
