using Microsoft.EntityFrameworkCore;
using Smoerfugl.Database.Postgres;
using Smoerfugl.Database.Postgres.BaseEntities;
using TooltrackerRfid.Database.Entities;

namespace TooltrackerRfid.Database
{
    public sealed class TooltrackerRfidDbContext : DbContextAbstraction<TooltrackerRfidDbContext>
    {
        public TooltrackerRfidDbContext(DbContextOptions<TooltrackerRfidDbContext> options) : base(options)
        {
        }

        public DbSet<RfidTag> RfidTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyGlobalFilters<IBaseEntity>(d => d.DeletedAt == null);
            modelBuilder.ApplyConfiguration(new RfidTagConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
    
    public class ContextFactory : DesignTimeDbContextFactory<TooltrackerRfidDbContext>.Postgres
    {
        public ContextFactory()
        {
            DatabaseName = "ToolTrackerRfid";
        }
    }
}