namespace ReinoTrebolApi.Models.Internal.Mappers
{
    using AutoMapper;

    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<Models.Internal.Registration, Models.Data.Registration>().ReverseMap();
        }
    }
}
