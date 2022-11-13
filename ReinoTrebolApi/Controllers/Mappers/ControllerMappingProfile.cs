using AutoMapper;
using Microsoft.AspNetCore.Routing.Matching;
using System.Linq;

namespace ReinoTrebolApi.Controllers.Mappers
{
    public class ControllerMappingProfile : Profile
    {
        public ControllerMappingProfile()
        {
            this.CreateMap<Models.Resource.SolicitudPost, Models.Internal.Solicitud>().ReverseMap();
            this.CreateMap<Models.Resource.SolicitudPatch, Models.Internal.Solicitud>().ReverseMap();
            this.CreateMap<Models.Resource.Solicitud, Models.Internal.Solicitud>().ReverseMap();
            this.CreateMap<Models.Resource.SolicitudResponseCollection , List<Models.Internal.Solicitud>>();
            this.CreateMap<List<Models.Internal.Solicitud>, Models.Resource.SolicitudResponseCollection>()
                .ForMember(dest => dest.Solicitudes, opt => opt.MapFrom(
                    sol => sol) );
            

            this.CreateMap<Models.Internal.Solicitud, Models.Resource.GrimorioAsignado>().ReverseMap();
        }
    }
}
