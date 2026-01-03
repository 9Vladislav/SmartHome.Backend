using FluentValidation;
using SmartHome.Core.DTO.Admin;

namespace SmartHome.Core.Validation.Admin;

public class SetUserStatusDtoValidator : AbstractValidator<SetUserStatusDto>
{
    public SetUserStatusDtoValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Некоректний статус користувача.");
    }
}
