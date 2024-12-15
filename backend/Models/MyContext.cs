using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    // the MyContext class represents a session with our MySQL database, allowing us to query for or save data
    // DbContext is a class that comes from EntityFramework, we want to inherit its features
    public class MyContext : DbContext
    {
        // This line will always be here. It is what constructs our context upon initialization
        public MyContext(DbContextOptions options) : base(options) { }
        // We need to create a new DbSet<Model> for every model in our project that is making a table
        // The name of our table in our database will be based on the name we provide here
        // This is where we provide a plural version of our model to fit table naming standards    
        public DbSet<Golfer> Golfers { get; set; }
        public DbSet<GolfCourse> GolfCourses { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring relationships and keys

            modelBuilder.Entity<Golfer>()
                .HasKey(g => g.GolferId);

            modelBuilder.Entity<GolfCourse>()
                .HasKey(gc => gc.GolfCourseID);

            modelBuilder.Entity<TimeSlot>()
                .HasKey(ts => ts.TimeSlotID);

            modelBuilder.Entity<Booking>()
                .HasKey(b => b.BookingId);

            // Relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Golfer)
                .WithMany(g => g.Bookings)
                .HasForeignKey(b => b.GolferId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.GolfCourse)
                .WithMany()
                .HasForeignKey(b => b.GolfCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.TimeSlot)
                .WithMany()
                .HasForeignKey(b => b.TimeSlotId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TimeSlot>()
                .Property(ts => ts.RemainingCapacity)
                .IsRequired();

            // Seeding data (optional for testing)
            // Example of a golf course:
            modelBuilder.Entity<GolfCourse>().HasData(
                new GolfCourse { GolfCourseID = 1, CourseName = "Ocean View", MaxPlayersPerSlot = 4 },
                new GolfCourse { GolfCourseID = 2, CourseName = "Mountain Ridge", MaxPlayersPerSlot = 4 },
                new GolfCourse { GolfCourseID = 3, CourseName = "Desert Dunes", MaxPlayersPerSlot = 4 }
            );
        }
    }
}
