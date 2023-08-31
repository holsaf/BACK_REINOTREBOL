namespace ReinoTrebolApi.Services.Registration
{
    using ReinoTrebolApi.Models.Internal;

    public interface IRegistrationService
    {
        Task<Registration> SaveRegistration(Registration registration, bool status);

        Task<Registration> UpdateRegistration(Registration registration);

        Task<List<Registration>> GetRegistrations();

        Task<Registration> GetRegistration(Guid id);

        Task<bool> DeleteRegistration(Guid id);
    }
}
