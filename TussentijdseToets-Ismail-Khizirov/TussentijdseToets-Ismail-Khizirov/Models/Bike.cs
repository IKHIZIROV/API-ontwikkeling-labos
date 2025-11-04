namespace TussentijdseToets_Ismail_Khizirov.Models
{
    public class Bike
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }

        public decimal PricePerHour { get; set; }

        public bool IsAvailable { get; set; }
    }
}
