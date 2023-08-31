namespace ReinoTrebolApi.Repository.Registration
{
    using ReinoTrebolApi.Models.Data;

    public interface IRegistrationRepository
    {
        Task<Registration> SaveRegistration(Registration registration);

        Task<Registration> UpdateRegistration(Registration registration);

        Task<List<Registration>> GetRegistrations();

        Task<Registration> GetRegistration(Guid id);

        Task<bool> DeleteRegistration(Guid id);
    }
}
