using Microsoft.EntityFrameworkCore;

namespace MvsMyTest.Models
{
    public class StuffContext : DbContext
    {
        public StuffContext()
        {
        }

        public StuffContext(DbContextOptions<StuffContext> options)
            : base(options)
        {
        }

        public DbSet<StuffItem> StuffItems { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<StuffItem>()
        //        .HasMany(c => c.Tags)
        //        .WithOne(e => e.Stuff)
        //        .OnDelete(DeleteBehavior.Cascade);
        //}
    }
}
