using ReinoTrebolApi.Repository.Solicitud;

namespace ReinoTrebolApi.Services.Solicitud
{
    public class SolicitudService : ISolicitudService
    {
        private readonly ISolicitudRepository solicitudRepository;
        public Task<Models.Internal.Solicitud> ActualizarEstado(Models.Resource.Solicitud solicitud)
        {
            solicitudRepository.ActualizarSolicitud(solicitud);
            
        }

        public Task<Models.Internal.Solicitud> ActualizarSolicitud(Models.Data.Solicitud solicitud)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Internal.Solicitud> CargarSolicitud(Models.Data.Solicitud solicitud)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Internal.Solicitud> ConsultarAsignacionGrimorio(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Internal.Solicitud>> ConsultarSolicitudes()
        {
            throw new NotImplementedException();
        }

        public Task<bool> EliminarSolicitud(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
