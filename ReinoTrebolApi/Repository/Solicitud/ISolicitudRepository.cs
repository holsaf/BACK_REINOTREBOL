namespace ReinoTrebolApi.Repository.Solicitud
{
    using ReinoTrebolApi.Models.Data;

    public interface ISolicitudRepository
    {
        Task<Solicitud> CargarSolicitud(Solicitud solicitud);

        Task<Solicitud> ActualizarSolicitud(Solicitud solicitud);

        Task<List<Solicitud>> ConsultarSolicitudes();

        Task<Solicitud> ConsultarSolicitud(Guid id);

        Task<Boolean> EliminarSolicitud(Guid id);
    }
}
