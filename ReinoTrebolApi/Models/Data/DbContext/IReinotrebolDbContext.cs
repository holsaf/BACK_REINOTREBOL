namespace ReinoTrebolApi.Models.Data.DbContext
{
    using Microsoft.EntityFrameworkCore;

    public interface IReinotrebolDbContext
    {
        DbSet<Registration> Registration { get; set; }

        Task<int> SaveChangesAsync();
    }
}
