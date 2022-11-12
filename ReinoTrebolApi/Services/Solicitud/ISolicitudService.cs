
namespace ReinoTrebolApi.Services.Solicitud
{
    public interface ISolicitudService
    {
        Task<Models.Internal.Solicitud> CargarSolicitud(Models.Data.Solicitud solicitud);

        Task<Models.Internal.Solicitud> ActualizarSolicitud(Models.Data.Solicitud solicitud);

        Task<Models.Internal.Solicitud> ActualizarEstado(Models.Data.Solicitud solicitud);

        Task<IEnumerable<Models.Internal.Solicitud>> ConsultarSolicitudes();

        Task<Models.Internal.Solicitud> ConsultarAsignacionGrimorio(Guid id);

        Task<Boolean> EliminarSolicitud(Guid id);
    }
}
