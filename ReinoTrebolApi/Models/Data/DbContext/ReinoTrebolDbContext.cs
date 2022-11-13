using Microsoft.EntityFrameworkCore;

namespace ReinoTrebolApi.Models.Data.DbContext
{
    using Microsoft.EntityFrameworkCore;

    public class ReinoTrebolDbContext : DbContext, IReinotrebolDbContext
    {
        public ReinoTrebolDbContext(DbContextOptions<ReinoTrebolDbContext> options)
            : base(options) { }


        public DbSet<Solicitud> Solicitud { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("ReinoTrebol");

            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.ToTable("Solicitud");
                entity.HasKey(id => id.IdSolicitud);
                //entity.HasIndex(entity => new { entity.Identificacion }).IsUnique();
            });

        }
    }
}
