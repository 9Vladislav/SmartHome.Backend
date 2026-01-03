using FluentValidation;
using SmartHome.Core.DTO.Events;

namespace SmartHome.Core.Validation.Events;

public class CreateEventDtoValidator : AbstractValidator<CreateEventDto>
{
    public CreateEventDtoValidator()
    {
        RuleFor(x => x.SensorId)
            .GreaterThan(0).WithMessage("SensorId має бути більше 0.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Некоректний тип події.");

        RuleFor(x => x.OccurredAt)
            .LessThanOrEqualTo(DateTime.UtcNow.AddMinutes(1))
            .When(x => x.OccurredAt.HasValue)
            .WithMessage("OccurredAt не може бути в майбутньому.");
    }
}
