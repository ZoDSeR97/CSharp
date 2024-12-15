namespace backend.Models
{
    public class TimeSlot
    {
        public int TimeSlotID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RemainingCapacity { get; set; }
        public int GolfCourseID { get; set; }

        public GolfCourse GolfCourse { get; set; }

        public bool IsAvailable()
        {
            return RemainingCapacity > 0;
        }

        public bool BookSlot(Golfer golfer)
        {
            if (IsAvailable())
            {
                RemainingCapacity--;
                return true;
            }
            return false;
        }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}