using FluentValidation;
using ReinoTrebolApi.Models.Resource;
using System.Runtime.CompilerServices;

namespace ReinoTrebolApi.Validator
{
    public class SolicitudPostValidator : AbstractValidator<SolicitudPost>
    {
        public SolicitudPostValidator()
        {
            this.RuleFor(sol => sol.Nombre).MaximumLength(20).Matches("^[a-zA-Z]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.Apellido).MaximumLength(20).Matches("^[a-zA-Z]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.Identificacion).MaximumLength(10).Matches("^[a-zA-Z0-9]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.Edad).MaximumLength(2).Matches("^[0-9]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.AfinidadMagica).IsInEnum().WithSeverity(Severity.Warning);
        }

    }
}
