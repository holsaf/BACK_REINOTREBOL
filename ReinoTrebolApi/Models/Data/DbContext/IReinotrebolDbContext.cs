using Microsoft.EntityFrameworkCore;

namespace ReinoTrebolApi.Models.Data.DbContext
{
    public interface IReinotrebolDbContext
    {
        DbSet<Solicitud> Solicitud { get; set; }

        Task<int> SaveChangesAsync();
    }
}
