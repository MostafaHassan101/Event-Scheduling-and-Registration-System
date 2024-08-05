using EventSystem.Application.Common.Models;
using EventSystem.Infrastructure.Services;
using MediatR;

namespace EventSystem.Application.UserMangment.Commands.LoginUser;
public record LoginUserCommand : IRequest<APIResponse>
{
	public string Email { get; set; }
	public string Password { get; set; }
	public bool RememberMe { get; set; }
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, APIResponse>
{
	private readonly IUserRegistrationService _userRegistrationService;

    public LoginUserCommandHandler(IUserRegistrationService userRegistrationService)
    {
        _userRegistrationService = userRegistrationService;
    }

    public async Task<APIResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
	{
		APIResponse response = new APIResponse();
		var token = await _userRegistrationService.LoginUserAsync(request.Email, request.Password, request.RememberMe);
		if (token == null)
		{
			response.AddError("", "Invalid email or password");
			return response;
		}

		response.AddMeta("token", token);
		return response;
	}
}