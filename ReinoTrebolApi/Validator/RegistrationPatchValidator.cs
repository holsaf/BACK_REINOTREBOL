using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using ReinoTrebolApi.Models.Resource;

namespace ReinoTrebolApi.Validator
{
    public class RegistrationPatchValidator : AbstractValidator<JsonPatchDocument<RegistrationPatch>>
    {
        public RegistrationPatchValidator()
        {
            this.RuleFor(sol => sol.Operations[0].value).Must(value => value.ToString() !.ToLower() == "approved" || value.ToString() !.ToLower() == "rejected").WithMessage("The 'Value' should be 'Approved' o 'Rejected'");
        }
    }
}