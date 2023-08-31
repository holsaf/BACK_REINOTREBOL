namespace ReinoTrebolApi.Controllers.Registrationes
{
    using System;
    using AutoMapper;
    using FluentValidation;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using ReinoTrebolApi.Models.Resource;
    using ReinoTrebolApi.Services.Registration;

    [ApiController]
    [Route("api/registration")]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService registrationService;
        private readonly IMapper mapper;
        private readonly IValidator<RegistrationPost> validatorPost;
        private readonly IValidator<Registration> validatorPut;
        private readonly IValidator<JsonPatchDocument<RegistrationPatch>> validatorPatch;

        public RegistrationController(
            IRegistrationService registrationService,
            IMapper mapper,
            IValidator<RegistrationPost> validatorPost,
            IValidator<JsonPatchDocument<RegistrationPatch>> validatorPatch,
            IValidator<Registration> validatorPut)
        {
            this.registrationService = registrationService;
            this.mapper = mapper;
            this.validatorPost = validatorPost;
            this.validatorPatch = validatorPatch;
            this.validatorPut = validatorPut;
        }

        [HttpPost(Name = nameof(PostRegistration))]
        [ProducesResponseType(typeof(Registration), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Registration), StatusCodes.Status400BadRequest)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> PostRegistration([FromBody] RegistrationPost registrationPost)
        {
            var validatorResult = this.validatorPost.Validate(registrationPost);
            if (registrationPost == null)
            {
                return this.BadRequest();
            }

            var registrationMapped = this.mapper.Map<Models.Internal.Registration>(registrationPost);

            if (!validatorResult.IsValid)
            {
                var registrationRechazada = await this.registrationService.SaveRegistration(registrationMapped, false);
                return this.BadRequest(this.mapper.Map<Registration>(registrationRechazada));
            }

            var registrationCreated = await this.registrationService.SaveRegistration(registrationMapped, true);
            return this.Created(nameof(this.PostRegistration), this.mapper.Map<Registration>(registrationCreated));
        }

        [HttpPut(Name = nameof(PutRegistration))]
        [ProducesResponseType(typeof(Registration), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> PutRegistration([FromBody] Registration registration)
        {
            var validatorResult = this.validatorPut.Validate(registration);
            if (!validatorResult.IsValid)
            {
                return this.BadRequest(validatorResult.Errors);
            }

            var internalRegistration = await this.registrationService.GetRegistration(registration.IdRegistration);

            if (internalRegistration == null)
            {
                return this.NotFound();
            }

            var registrationMapped = this.mapper.Map<Models.Internal.Registration>(registration);
            var registrationUpdated = await this.registrationService.UpdateRegistration(registrationMapped);
            return this.Ok(this.mapper.Map<Registration>(registrationUpdated));
        }

        [HttpPatch("{id}", Name = nameof(PatchRegistration))]
        [ProducesResponseType(typeof(Registration), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json-patch+json")]
        [Produces("application/json")]
        public async Task<IActionResult> PatchRegistration(Guid id, [FromBody] JsonPatchDocument<RegistrationPatch> registrationPatch)
        {
            var validatorResult = this.validatorPatch.Validate(registrationPatch);
            if (!validatorResult.IsValid)
            {
                return this.BadRequest(validatorResult.Errors);
            }

            var internalRegistration = await this.registrationService.GetRegistration(id);

            if (internalRegistration == null)
            {
                return this.NotFound();
            }

            var baseRegistrationPatch = this.mapper.Map<RegistrationPatch>(internalRegistration);
            try
            {
                registrationPatch.ApplyTo(baseRegistrationPatch, this.ModelState);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, e);
            }

            var registrationMapped = this.mapper.Map<Models.Internal.Registration>(baseRegistrationPatch);
            registrationMapped.IdRegistration = id;
            var registrationUpdated = await this.registrationService.UpdateRegistration(registrationMapped);
            return this.Ok(this.mapper.Map<Registration>(registrationUpdated));
        }

        [HttpGet(Name= nameof(GetRegistrationes))]
        [ProducesResponseType(typeof(RegistrationResponseCollection), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> GetRegistrationes()
        {
            var registrationes = await this.registrationService.GetRegistrations();
            if (registrationes == null)
            {
                return this.NotFound();
            }

            return this.Ok(this.mapper.Map<RegistrationResponseCollection>(registrationes));
        }

        [HttpGet("grimorio/{id}")]
        [ProducesResponseType(typeof(SportChosen), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> GetGrimorioById(Guid id)
        {
            var registration = await this.registrationService.GetRegistration(id);
            if (registration == null)
            {
                return this.NotFound();
            }

            return this.Ok(this.mapper.Map<Models.Resource.SportChosen>(registration));
        }

        [HttpDelete("{id}", Name = nameof(DeleteRegistration))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteRegistration(Guid id)
        {
            var internalRegistration = await this.registrationService.GetRegistration(id);
            if (internalRegistration == null)
            {
                return this.NotFound();
            }

            var result = await this.registrationService.DeleteRegistration(id);

            if (result)
            {
                return this.Ok();
            }
            else
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
