
using ReinoTrebolApi.Models.Internal;

namespace ReinoTrebolApi.Services.Solicitud
{
    public interface ISolicitudService
    {
        Task<ReinoTrebolApi.Models.Internal.Solicitud> CargarSolicitud(Models.Data.Solicitud solicitud);

        Task<ReinoTrebolApi.Models.Internal.Solicitud> ActualizarSolicitud(Models.Data.Solicitud solicitud);

        //Task<Solicitud> ActualizarEstado(Solicitud solicitud);

        Task<IEnumerable<ReinoTrebolApi.Models.Internal.Solicitud>> ConsultarSolicitudes();

        Task<ReinoTrebolApi.Models.Internal.Solicitud> ConsultarAsignacionGrimorio(Guid id);

        Task<Boolean> EliminarSolicitud(Guid id);
    }
}
