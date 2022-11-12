using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReinoTrebolApi.Models.Resource;
using ReinoTrebolApi.Services.Solicitud;

namespace ReinoTrebolApi.Controllers.Solicitudes
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudController : Controller
    {
        private readonly ISolicitudService solicitudService;
        private readonly IMapper mapper;

        public SolicitudController(ISolicitudService solicitudService, IMapper mapper)
        {
            this.solicitudService = solicitudService;
            this.mapper = mapper;
        }

        [HttpPost(Name = nameof(PostSolicitud))]
        [ProducesResponseType(typeof(Solicitud), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostSolicitud([FromBody] SolicitudPost solicitudPost)
        {
            if (solicitudPost == null)
            {
                return this.BadRequest();
            }
            var solicitudMapped = this.mapper.Map<Models.Internal.Solicitud>(solicitudPost);
            if (!this.TryValidateModel(solicitudPost))
            {
                var solicitudRechazada = await this.solicitudService.CargarSolicitud(solicitudMapped, false);
                return this.BadRequest(this.mapper.Map<Solicitud>(solicitudRechazada));
            }

            var solicitudCreated = await this.solicitudService.CargarSolicitud(solicitudMapped, true);
            return this.Created(nameof(PostSolicitud), this.mapper.Map<Solicitud>(solicitudCreated));

        }

        [HttpPut(Name = nameof(UpdateSolicitud))]
        [ProducesResponseType(typeof(Solicitud), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSolicitud([FromBody] Solicitud solicitud)
        {
            if (!this.TryValidateModel(solicitud))
            {
                return this.BadRequest();
            }
            var solicitudMapped = this.mapper.Map<Models.Internal.Solicitud>(solicitud);
            var solicitudUpdated = await this.solicitudService.ActualizarSolicitud(solicitudMapped);
            return this.Ok(this.mapper.Map<Solicitud>(solicitudUpdated));
        }

        [HttpPatch("{id}", Name = nameof(UpdateEstado))]
        [ProducesResponseType(typeof(Solicitud), StatusCodes.Status200OK)]
        [Consumes("application/json-patch+json")]
        public async Task<IActionResult> UpdateEstado(Guid id, [FromBody] JsonPatchDocument<SolicitudPatch> solicitudPatch)
        {
            if (!this.TryValidateModel(solicitudPatch))
            {
                return this.BadRequest();
            }

            var internalSolicitud = await this.solicitudService.ConsultarSolicitud(id);
            var baseSolicitudPatch = this.mapper.Map<Models.Resource.SolicitudPatch>(internalSolicitud);
            try
            {
                solicitudPatch.ApplyTo(baseSolicitudPatch, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)this.ModelState);
            }
            catch (Exception)
            {
                throw;
            }

            var solicitudMapped = this.mapper.Map<Models.Internal.Solicitud>(baseSolicitudPatch);
            solicitudMapped.IdSolicitud = id;
            var solicitudUpdated = await this.solicitudService.ActualizarSolicitud(solicitudMapped);
            return this.Ok(this.mapper.Map<Solicitud>(solicitudUpdated));
        }

        [HttpGet(Name= nameof(GetSolicitudes))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSolicitudes()
        {
            var solicitudes = await this.solicitudService.ConsultarSolicitudes();
            return this.Ok(this.mapper.Map<SolicitudResponseCollection>(solicitudes));
        }

        [HttpGet("{id}", Name = nameof(GetGrimorioById))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("GetGrimorio")]
        public async Task<IActionResult> GetGrimorioById(Guid id)
        {
            var solicitud = await this.solicitudService.ConsultarSolicitud(id);
            return this.Ok(this.mapper.Map<Models.Resource.GrimorioAsignado>(solicitud));

        }

        [HttpDelete("{id}", Name = nameof(DeleteSolicitud))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSolicitud(Guid id)
        {
            var result = await this.solicitudService.EliminarSolicitud(id);
            if (result)
            {
                return this.Ok();
            }
            else 
            {
                return this.BadRequest();
            }
        }



    }
}
