using FluentValidation;
using SmartHome.Core.DTO.Profile;
using System.Text.RegularExpressions;

namespace SmartHome.Core.Validation.Profile;

public class UpdateProfileDtoValidator : AbstractValidator<UpdateProfileDto>
{
    private static readonly Regex UaPhoneRegex = new(@"^\+380\d{9}$", RegexOptions.Compiled);

    private static readonly Regex EmailRegex = new(
        @"^[A-Za-z0-9._%+\-]+@[A-Za-z0-9.\-]+\.[A-Za-z]{2,}$",
        RegexOptions.Compiled
    );

    public UpdateProfileDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ім'я є обов'язковим.")
            .MaximumLength(100).WithMessage("Ім'я надто довге.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Прізвище обов'язкове.")
            .MaximumLength(100).WithMessage("Прізвище надто довге.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Пошта обов'язкова.")
            .Must(BeValidEmail).WithMessage("Некоректна пошта.")
            .MaximumLength(255).WithMessage("Пошта надто довга.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Номер телефону є обов'язковим.")
            .Must(BeUaPhone).WithMessage("Номер телефону має бути у форматі +380XXXXXXXXX.");
    }

    private static bool BeUaPhone(string phone)
    {
        phone = (phone ?? "").Trim();
        return UaPhoneRegex.IsMatch(phone);
    }

    private static bool BeValidEmail(string email)
    {
        email = (email ?? "").Trim();
        return EmailRegex.IsMatch(email);
    }
}