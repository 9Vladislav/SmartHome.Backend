using FluentValidation;
using SmartHome.Core.DTO.Auth;
using System.Text.RegularExpressions;

namespace SmartHome.Core.Validation.Auth;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    private static readonly Regex UaPhoneRegex =
        new(@"^\+380\d{9}$", RegexOptions.Compiled);

    private static readonly Regex EmailRegex =
        new(@"^[A-Za-z0-9._%+\-]+@[A-Za-z0-9.\-]+\.[A-Za-z]{2,}$",
            RegexOptions.Compiled);

    public LoginDtoValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Логін обов'язковий.")
            .Must(BeEmailOrUaPhone)
            .WithMessage("Неправильний формат. Введіть пошту або номер у форматі +380XXXXXXXXX.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обов'язковий.");
    }

    private static bool BeEmailOrUaPhone(string login)
    {
        login = (login ?? "").Trim();
        return EmailRegex.IsMatch(login) || UaPhoneRegex.IsMatch(login);
    }
}
