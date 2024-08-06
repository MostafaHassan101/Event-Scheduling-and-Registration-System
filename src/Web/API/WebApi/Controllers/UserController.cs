using Azure;
using EventSystem.Application.Common.Models;
using EventSystem.Application.EventManagement.Commands.CreateEventCommand;
using EventSystem.Application.UserMangment.Commands.LoginUser;
using EventSystem.Application.UserMangment.Commands.RegisterUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ApiControllerBase
    {
        /// <summary>
        /// Endpoints
        /// 	Register User
        /// 	Login User
        /// </summary>


        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand createEventCommand)
        {
            await Mediator.Send(createEventCommand);
            return Ok();
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand loginUserCommand)
        {
            var response = await Mediator.Send(loginUserCommand);
            switch (response.StatusCode)
            {
                case AppConstants.Success:
                    return Ok(response.responsebody);
                case AppConstants.UnAuthorized:
                    return Unauthorized(response.responsebody);
                case AppConstants.NotFound:
                    return NotFound(response.responsebody);
                default:
                    return BadRequest(response.responsebody);
            }
        }
    }
}
