namespace backend.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int GolferId { get; set; }
        public int GolfCourseId { get; set; }
        public int TimeSlotId { get; set; }
        public DateTime BookingDateTime { get; set; }

        public Golfer Golfer { get; set; }
        public GolfCourse GolfCourse { get; set; }
        public TimeSlot TimeSlot { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}