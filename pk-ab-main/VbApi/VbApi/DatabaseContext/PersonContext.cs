using Microsoft.EntityFrameworkCore;
using VbApi.Entity;

namespace VbApi.DatabaseContext
{
    public class PersonContext : DbContext
    {
        public DbSet<Staff> Types { get; set; }
        public DbSet<Employee> Status { get; set; }
        public PersonContext(DbContextOptions options) : base(options)
        {

        }
    }
}
