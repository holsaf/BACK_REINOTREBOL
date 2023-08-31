namespace ReinoTrebolApi.Repository.Registration
{
    using Microsoft.EntityFrameworkCore;
    using ReinoTrebolApi.Models.Data;
    using ReinoTrebolApi.Models.Data.DbContext;

    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly IReinotrebolDbContext reinoTrebolDbContext;

        public RegistrationRepository(IReinotrebolDbContext reinoTrebolDbContext)
        {
            this.reinoTrebolDbContext = reinoTrebolDbContext;
        }

        public async Task<Registration> UpdateRegistration(Registration registration)
        {
            try
            {
                var registrationToUpdate = await this.reinoTrebolDbContext.Registration.FirstAsync(s => s.IdRegistration == registration.IdRegistration);
                registrationToUpdate.Name = registration.Name;
                registrationToUpdate.LastName = registration.LastName;
                registrationToUpdate.Identification = registration.Identification;
                registrationToUpdate.Age = registration.Age;
                registrationToUpdate.Art = registration.Art;
                registrationToUpdate.Status = registration.Status;
                registrationToUpdate.Sport = registration.Sport;

                await this.reinoTrebolDbContext.SaveChangesAsync();

                return registrationToUpdate;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Registration> SaveRegistration(Registration registration)
        {
            try
            {
                await this.reinoTrebolDbContext.Registration.AddAsync(registration);
                await this.reinoTrebolDbContext.SaveChangesAsync();
                return registration;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Registration> GetRegistration(Guid id)
        {
            try
            {
                return await this.reinoTrebolDbContext.Registration.FirstOrDefaultAsync(s => s.IdRegistration == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Registration>> GetRegistrations()
        {
            try
            {
                return await this.reinoTrebolDbContext.Registration.ToListAsync<Registration>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteRegistration(Guid id)
        {
            try
            {
                var registrationDeleted = await this.reinoTrebolDbContext.Registration.FirstAsync(s => s.IdRegistration == id);
                this.reinoTrebolDbContext.Registration.Remove(registrationDeleted);
                await this.reinoTrebolDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
