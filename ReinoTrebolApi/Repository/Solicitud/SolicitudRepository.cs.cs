namespace ReinoTrebolApi.Repository.Solicitud
{
    using Microsoft.EntityFrameworkCore;
    using ReinoTrebolApi.Models.Data;
    using ReinoTrebolApi.Models.Data.DbContext;
    using System.Linq;

    public class SolicitudRepository : ISolicitudRepository
    {
        private readonly IReinotrebolDbContext reinoTrebolDbContext;

        public SolicitudRepository(IReinotrebolDbContext reinoTrebolDbContext)
        {
            this.reinoTrebolDbContext = reinoTrebolDbContext;
        }

        public async Task<Solicitud> ActualizarSolicitud(Solicitud solicitud)
        {
            try
            {
                var solicitudToUpdate = await this.reinoTrebolDbContext.Solicitud.FirstAsync(s => s.IdSolicitud == solicitud.IdSolicitud);
                solicitudToUpdate.Nombre = solicitud.Nombre;
                solicitudToUpdate.Apellido = solicitud.Apellido;
                solicitudToUpdate.Identificacion = solicitud.Identificacion;
                solicitudToUpdate.Edad = solicitud.Edad;
                solicitudToUpdate.AfinidadMagica = solicitud.AfinidadMagica;
                solicitudToUpdate.Estado = solicitud.Estado;
                solicitudToUpdate.Grimorio = solicitud.Grimorio;

                await this.reinoTrebolDbContext.SaveChangesAsync();

                return solicitudToUpdate;
             
            }
            catch (Exception)
            {             
                throw;
            }

        }

        //public Task<Solicitud> ActualizarEstado (Solicitud solicitud)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Solicitud> CargarSolicitud(Solicitud solicitud)
        {
            try
            {
                await this.reinoTrebolDbContext.Solicitud.AddAsync(solicitud);
                await this.reinoTrebolDbContext.SaveChangesAsync();
                return solicitud;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Solicitud> ConsultarSolicitud(Guid id)
        {
            try
            {
                return await this.reinoTrebolDbContext.Solicitud.FirstAsync(s => s.IdSolicitud == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Solicitud>> ConsultarSolicitudes()
        {
            try
            {

                return await this.reinoTrebolDbContext.Solicitud.ToListAsync<Solicitud>();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> EliminarSolicitud(Guid id)
        {
            try
            {
                var solicitudDeleted = await  this.reinoTrebolDbContext.Solicitud.FirstAsync(s => s.IdSolicitud == id);
                this.reinoTrebolDbContext.Solicitud.Remove(solicitudDeleted);
                await this.reinoTrebolDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
