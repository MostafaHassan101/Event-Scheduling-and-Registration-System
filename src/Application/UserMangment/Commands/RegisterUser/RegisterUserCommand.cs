using AutoMapper;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Domain.ValueObjects;
using MediatR;

namespace EventSystem.Application.UserMangment.Commands.RegisterUser;

public record RegisterUserCommand : IRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PrimaryPhoneNumber { get; set; }
    public string SecondaryEmail { get; set; }
    public string LinkedIn {  get; set; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
	private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
	{
        //await _userRepository.AddAsync(request);
    }
}
