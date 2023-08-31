namespace ReinoTrebolApi.Services.Registration
{
    using AutoMapper;
    using ReinoTrebolApi.Repository.Registration;

    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository registrationRepository;
        private readonly IMapper mapper;

        public RegistrationService(IRegistrationRepository registrationRepository, IMapper mapper)
        {
            this.registrationRepository = registrationRepository;
            this.mapper = mapper;
        }

        public async Task<Models.Internal.Registration> UpdateRegistration(Models.Internal.Registration registration)
        {
            var registrationMapped = this.mapper.Map<Models.Data.Registration>(registration);
            var registrationUpdated = await this.registrationRepository.UpdateRegistration(registrationMapped);
            return this.mapper.Map<Models.Internal.Registration>(registrationUpdated);
        }

        public async Task<Models.Internal.Registration> SaveRegistration(Models.Internal.Registration registration, bool estado)
        {
            var random = new Random();
            var registrationMapped = this.mapper.Map<Models.Data.Registration>(registration);
            if (estado == true)
            {
                registrationMapped.Status = Models.Enums.RegistrationStatus.Approved;
                registrationMapped.Sport = (Models.Enums.SportType)random.Next(1, 5);
                var registrationCreated = await this.registrationRepository.SaveRegistration(registrationMapped);
                return this.mapper.Map<Models.Internal.Registration>(registrationCreated);
            }
            else
            {
                registrationMapped.Status = Models.Enums.RegistrationStatus.Rejected;
                var registrationRechazada = await this.registrationRepository.SaveRegistration(registrationMapped);
                return this.mapper.Map<Models.Internal.Registration>(registrationRechazada);
            }
        }

        public async Task<Models.Internal.Registration> GetRegistration(Guid id)
        {
            var registrationResult = await this.registrationRepository.GetRegistration(id);
            return this.mapper.Map<Models.Internal.Registration>(registrationResult);
        }

        public async Task<List<Models.Internal.Registration>> GetRegistrations()
        {
            var registrationesResult = await this.registrationRepository.GetRegistrations();
            return this.mapper.Map<List<Models.Data.Registration>, List<Models.Internal.Registration>>(registrationesResult);
        }

        public Task<bool> DeleteRegistration(Guid id)
        {
            return this.registrationRepository.DeleteRegistration(id);
        }
    }
}
