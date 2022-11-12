using AutoMapper;

namespace ReinoTrebolApi.Controllers.Mappers
{
    public class ControllerMappingProfile : Profile
    {
        public ControllerMappingProfile()
        {
            this.CreateMap<Models.Resource.SolicitudPost, Models.Internal.Solicitud>().ReverseMap();
            this.CreateMap<Models.Resource.SolicitudPatch, Models.Internal.Solicitud>().ReverseMap();
            this.CreateMap<Models.Resource.Solicitud, Models.Internal.Solicitud>().ReverseMap();
            this.CreateMap<Models.Resource.SolicitudResponseCollection , IEnumerable<Models.Internal.Solicitud>>().ReverseMap();
            this.CreateMap<Models.Internal.Solicitud, Models.Resource.GrimorioAsignado>().ReverseMap();
        }
    }
}
