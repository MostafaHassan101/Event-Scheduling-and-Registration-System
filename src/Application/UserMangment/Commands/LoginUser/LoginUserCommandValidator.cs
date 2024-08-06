using FluentValidation;

namespace EventSystem.Application.UserMangment.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(u => u.Email).NotEmpty().NotNull().EmailAddress();

        RuleFor(u => u.Password).NotEmpty().MinimumLength(8);
    }
}
