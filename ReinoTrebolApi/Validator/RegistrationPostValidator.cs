namespace ReinoTrebolApi.Validator
{
    using FluentValidation;
    using ReinoTrebolApi.Models.Resource;

    public class RegistrationPostValidator : AbstractValidator<RegistrationPost>
    {
        public RegistrationPostValidator()
        {
            this.RuleFor(sol => sol.Name).NotEmpty().MaximumLength(20).Matches("^[a-zA-Z]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.LastName).NotEmpty().MaximumLength(20).Matches("^[a-zA-Z]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.Identification).NotEmpty().MaximumLength(10).Matches("^[a-zA-Z0-9]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.Age).NotEmpty().MaximumLength(2).Matches("^[0-9]*$").WithSeverity(Severity.Warning);
            this.RuleFor(sol => sol.Art).NotEmpty().IsInEnum().WithSeverity(Severity.Warning);
        }

    }
}
