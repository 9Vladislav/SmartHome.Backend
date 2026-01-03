using FluentValidation;
using SmartHome.Core.DTO.Rooms;

namespace SmartHome.Core.Validation.Rooms;

public class UpdateRoomDtoValidator : AbstractValidator<UpdateRoomDto>
{
    public UpdateRoomDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Назва кімнати є обов'язковою.")
            .MaximumLength(100).WithMessage("Назва кімнати занадто довга (макс. 100).");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Опис занадто довгий (макс. 200).");
    }
}
