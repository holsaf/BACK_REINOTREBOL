namespace ReinoTrebolApi.Models.Data.DbContext
{
    using Microsoft.EntityFrameworkCore;

    public class ReinoTrebolDbContext : DbContext, IReinotrebolDbContext
    {
        public ReinoTrebolDbContext(DbContextOptions<ReinoTrebolDbContext> options)
            : base(options)
        {
        }

        public DbSet<Registration> Registration { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("ReinoTrebol");

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("Registration");
                entity.HasKey(id => id.IdRegistration);
            });
        }
    }
}
