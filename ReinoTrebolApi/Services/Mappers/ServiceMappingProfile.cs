using AutoMapper;


namespace ReinoTrebolApi.Services.Mappers
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<Models.Internal.Solicitud, Models.Data.Solicitud>().ReverseMap();

        }

    }
}
