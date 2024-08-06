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
		try
		{
			var token = await _userRegistrationService.LoginUserAsync(request.Email, request.Password, request.RememberMe);
			if (token == null)
			{
                response.clearBody();
                response.StatusCode = AppConstants.NotFound;
                response.AddError(AppConstants.Error, "Invalid email or password");
                return response;
			}
            response.clearBody();
            response.StatusCode = AppConstants.Success;
            response.AddModel(AppConstants.Model, token);
            response.AddMeta(AppConstants.Message, "logged in Successfully");
            return response;
        }
        catch (Exception ex) 
		{
            response.clearBody();
            response.StatusCode = AppConstants.NotFound;
            response.AddError(AppConstants.Error, "faild to login please try again later");
			return response;

        }
	}
}