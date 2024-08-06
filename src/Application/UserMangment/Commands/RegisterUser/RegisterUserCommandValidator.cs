using FluentValidation;

namespace EventSystem.Application.UserMangment.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(u => u.Email).NotEmpty().NotNull().EmailAddress();

            RuleFor(u => u.PrimaryPhoneNumber).NotEmpty().MinimumLength(11).MaximumLength(13);
        }
    }
}
