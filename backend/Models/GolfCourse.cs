namespace backend.Models
{
    public class GolfCourse
    {
        public int GolfCourseID { get; set; }
        public string CourseName { get; set; }
        public int MaxPlayersPerSlot { get; set; }

        public List<TimeSlot> AvailableSlots { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}