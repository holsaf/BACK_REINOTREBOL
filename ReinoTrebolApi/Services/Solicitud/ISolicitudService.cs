
namespace ReinoTrebolApi.Services.Solicitud
{
    using ReinoTrebolApi.Models.Internal;
    public interface ISolicitudService
    {
        Task<Solicitud> CargarSolicitud(Solicitud solicitud, Boolean estado);

        Task<Solicitud> ActualizarSolicitud(Solicitud solicitud);

        //Task<Solicitud> ActualizarEstado(Solicitud solicitud);

        Task<List<Solicitud>> ConsultarSolicitudes();

        //Task<Solicitud> ConsultarAsignacionGrimorio(Guid id);

        Task<Solicitud> ConsultarSolicitud(Guid id);

        Task<Boolean> EliminarSolicitud(Guid id);
    }
}
