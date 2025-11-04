namespace TussentijdseToets_Ismail_Khizirov.Models
{
    public class Rental
    {
        public int Id { get; set; }

        public int BikeId { get; set; }

        public string CustomerName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public decimal TotalCost { get; set; }
    }
}
