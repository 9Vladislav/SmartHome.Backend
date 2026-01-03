using FluentValidation;
using SmartHome.Core.DTO.Admin;
using System.Text.RegularExpressions;

namespace SmartHome.Core.Validation.Admin;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    private static readonly Regex UaPhoneRegex =
        new(@"^\+380\d{9}$", RegexOptions.Compiled);

    private static readonly Regex EmailRegex =
        new(@"^[A-Za-z0-9._%+\-]+@[A-Za-z0-9.\-]+\.[A-Za-z]{2,}$",
            RegexOptions.Compiled);

    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty().MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .Must(e => EmailRegex.IsMatch(e.Trim()))
            .WithMessage("Некоректна пошта.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Must(p => UaPhoneRegex.IsMatch(p.Trim()))
            .WithMessage("Телефон має бути у форматі +380XXXXXXXXX.");
    }
}
