namespace ReinoTrebolApi.Models.Data.DbContext
{
    using Microsoft.EntityFrameworkCore;

    public interface IReinotrebolDbContext
    {
        DbSet<Solicitud> Solicitud { get; set; }

        Task<int> SaveChangesAsync();
    }
}
