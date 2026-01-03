using FluentValidation;
using SmartHome.Core.DTO.Sensors;

namespace SmartHome.Core.Validation.Sensors;

public class SetSensorEnabledDtoValidator : AbstractValidator<SetSensorEnabledDto>
{
    public SetSensorEnabledDtoValidator()
    {
        RuleFor(x => x.IsEnabled)
            .NotNull().WithMessage("IsEnabled обов'язковий.");
    }
}
