using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace backend.Models
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            // Configure DbContextOptions with your connection string
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseMySql(
                "Server=localhost;port=3306;userid=root;password=root;database=Golfbox;",
                new MySqlServerVersion(new Version(8, 0, 21))
            );

            return new MyContext(optionsBuilder.Options);
        }
    }
}