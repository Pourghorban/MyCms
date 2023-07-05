using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DataLayer
{
    public class MyCmsContext : DbContext
    {


        public MyCmsContext(DbContextOptions options):base(options) 
        {
            
        }
        public DbSet<PageGroup> PageGroups { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageComment> PageComments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Page>(e =>
            {
                e.HasOne(p => p.PageGroup)
                    .WithMany(p => p.Pages)
                    .HasForeignKey(p => p.GroupID);
            });

            modelBuilder.Entity<PageComment>(e =>
            {
                 e.HasOne(p => p.Page)
             .WithMany(p => p.PageComments)
             .HasForeignKey(p => p.PageID);
            });
        }
    }
}
