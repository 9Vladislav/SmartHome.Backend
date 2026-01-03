using FluentValidation;
using SmartHome.Core.DTO.Security;

namespace SmartHome.Core.Validation.Security;

public class SetSecurityModeDtoValidator : AbstractValidator<SetSecurityModeDto>
{
    public SetSecurityModeDtoValidator()
    {
        RuleFor(x => x.Mode)
            .IsInEnum().WithMessage("Режим охорони має бути ARMED або DISARMED.");
    }
}
