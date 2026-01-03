using FluentValidation;
using SmartHome.Core.DTO.Sensors;

namespace SmartHome.Core.Validation.Sensors;

public class UpdateSensorDtoValidator : AbstractValidator<UpdateSensorDto>
{
    public UpdateSensorDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Назва сенсора є обов'язковою.")
            .MaximumLength(100).WithMessage("Назва сенсора занадто довга (макс. 100).");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Опис занадто довгий (макс. 200).");
    }
}
