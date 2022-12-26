using Microsoft.EntityFrameworkCore;

namespace ManagerFamily.Model.Data
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<SpendingCategory> SpendingCategories { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ManageStaffDBAppDB1;Trusted_Connection=True;");
        }
    }
}
