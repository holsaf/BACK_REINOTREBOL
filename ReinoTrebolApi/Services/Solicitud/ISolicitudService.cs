namespace ReinoTrebolApi.Services.Solicitud
{
    using ReinoTrebolApi.Models.Internal;

    public interface ISolicitudService
    {
        Task<Solicitud> CargarSolicitud(Solicitud solicitud, bool estado);

        Task<Solicitud> ActualizarSolicitud(Solicitud solicitud);

        Task<List<Solicitud>> ConsultarSolicitudes();

        Task<Solicitud> ConsultarSolicitud(Guid id);

        Task<bool> EliminarSolicitud(Guid id);
    }
}
