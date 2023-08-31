namespace ReinoTrebolApi.Models.Resource.Mappers
{
    using AutoMapper;

    public class ControllerMappingProfile : Profile
    {
        public ControllerMappingProfile()
        {
            this.CreateMap<Models.Resource.RegistrationPost, Models.Internal.Registration>().ReverseMap();
            this.CreateMap<Models.Resource.RegistrationPatch, Models.Internal.Registration>().ReverseMap();
            this.CreateMap<Models.Resource.Registration, Models.Internal.Registration>().ReverseMap();
            this.CreateMap<List<Models.Internal.Registration>, Models.Resource.RegistrationResponseCollection>()
                .ForMember(dest => dest.Registrations, opt => opt.MapFrom(sol => sol));
            this.CreateMap<Models.Internal.Registration, Models.Resource.SportChosen>().ReverseMap();
        }
    }
}
