using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DataLayer
{
    public class MyCmsContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public MyCmsContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<PageGroup> PageGroups { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageComment> PageComments { get; set; }

    }
}
