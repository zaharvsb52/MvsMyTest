using Microsoft.EntityFrameworkCore;

namespace MvsMyTest.Models
{
    public class TagItemContext : DbContext
    {
        public TagItemContext()
        {
        }

        public TagItemContext(DbContextOptions<TagItemContext> options)
            : base(options)
        {
        }

        public DbSet<TagItem> TagItems { get; set; }
    }
}
