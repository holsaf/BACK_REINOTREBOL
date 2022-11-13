namespace ReinoTrebolApi.Controllers.Solicitudes
{
    using AutoMapper;
    using FluentValidation;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using ReinoTrebolApi.Models.Resource;
    using ReinoTrebolApi.Services.Solicitud;

    [ApiController]
    [Route("api/solicitud")]
    public class SolicitudController : Controller
    {
        private readonly ISolicitudService solicitudService;
        private readonly IMapper mapper;
        private readonly IValidator<SolicitudPost> validator;

        public SolicitudController(ISolicitudService solicitudService, IMapper mapper, IValidator<SolicitudPost> validator)
        {
            this.solicitudService = solicitudService;
            this.mapper = mapper;
            this.validator = validator;
        }

        [HttpPost(Name = nameof(PostSolicitud))]
        [ProducesResponseType(typeof(Solicitud), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> PostSolicitud([FromBody] SolicitudPost solicitudPost)
        {
            var validatorResult = this.validator.Validate(solicitudPost);
            if (solicitudPost == null)
            {
                return this.BadRequest();
            }

            var solicitudMapped = this.mapper.Map<Models.Internal.Solicitud>(solicitudPost);

            if (!validatorResult.IsValid)
            {
                var solicitudRechazada = await this.solicitudService.CargarSolicitud(solicitudMapped, false);
                return this.BadRequest(this.mapper.Map<Solicitud>(solicitudRechazada));
            }

            var solicitudCreated = await this.solicitudService.CargarSolicitud(solicitudMapped, true);
            return this.Created(nameof(this.PostSolicitud), this.mapper.Map<Solicitud>(solicitudCreated));
        }

        [HttpPut(Name = nameof(PutSolicitud))]
        [ProducesResponseType(typeof(Solicitud), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> PutSolicitud([FromBody] Solicitud solicitud)
        {
            if (!this.TryValidateModel(solicitud))
            {
                return this.BadRequest();
            }
            var solicitudMapped = this.mapper.Map<Models.Internal.Solicitud>(solicitud);
            var solicitudUpdated = await this.solicitudService.ActualizarSolicitud(solicitudMapped);
            return this.Ok(this.mapper.Map<Solicitud>(solicitudUpdated));
        }

        [HttpPatch("{id}", Name = nameof(PatchEstado))]
        [ProducesResponseType(typeof(Solicitud), StatusCodes.Status200OK)]
        [Consumes("application/json-patch+json")]
        [Produces("application/json")]
        public async Task<IActionResult> PatchEstado(Guid id, [FromBody] JsonPatchDocument<SolicitudPatch> solicitudPatch)
        {
            if (!this.TryValidateModel(solicitudPatch))
            {
                return this.BadRequest();
            }

            var internalSolicitud = await this.solicitudService.ConsultarSolicitud(id);
            var baseSolicitudPatch = this.mapper.Map<Models.Resource.SolicitudPatch>(internalSolicitud);
            try
            {
                solicitudPatch.ApplyTo(baseSolicitudPatch, this.ModelState);
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
        [ProducesResponseType(typeof(SolicitudResponseCollection), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> GetSolicitudes()
        {
            var solicitudes = await this.solicitudService.ConsultarSolicitudes();
            return this.Ok(this.mapper.Map<SolicitudResponseCollection>(solicitudes));
        }

        [HttpGet("grimorio/{id}")]
        [ProducesResponseType(typeof(GrimorioAsignado), StatusCodes.Status200OK)]
        [Produces("application/json")]
        public async Task<IActionResult> GetGrimorioById(Guid id)
        {
            var solicitud = await this.solicitudService.ConsultarSolicitud(id);
            return this.Ok(this.mapper.Map<Models.Resource.GrimorioAsignado>(solicitud));

        }

        [HttpDelete("{id}", Name = nameof(DeleteSolicitud))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
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
