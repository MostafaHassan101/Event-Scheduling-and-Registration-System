using FluentValidation;

namespace EventSystem.Application.EventManagement.Commands.CreateEventCommand
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator()
        {
            RuleFor(c => c.Title).NotEmpty().NotNull();
            RuleFor(c => c.Address).NotEmpty().NotNull();

            RuleFor(c => c.Date).Must(c => c.Date > DateTime.UtcNow);
            RuleFor(e => e.Time).NotEmpty().NotNull();


        }
    }
}
