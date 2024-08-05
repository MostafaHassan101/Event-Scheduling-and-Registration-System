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
            return Ok(await Mediator.Send(loginUserCommand));
        }
    }
}
