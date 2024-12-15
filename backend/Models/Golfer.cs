namespace backend.Models
{
    public class Golfer
    {
        public int GolferId { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }

        public List<Booking> Bookings { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}