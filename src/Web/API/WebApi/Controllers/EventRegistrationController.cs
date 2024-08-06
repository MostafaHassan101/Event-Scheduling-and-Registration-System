using EventSystem.Application.EventRegistration.Commands.CancelUserRegistrationFromEvent;
using EventSystem.Application.EventRegistration.Commands.RegisterUserToEvent;
using EventSystem.Application.EventRegistration.Queries.GetAllEventsForUser;
using EventSystem.Application.EventRegistration.Queries.GetAllUsersForEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class EventRegistrationController : ApiControllerBase
    {
        /// <summary>
        /// Endpoints
        /// 	Register User for Event
        /// 	Cancel User Registration from Event
        /// 	List all Events for a User
        /// 	List all Users for an Event
        /// </summary>

        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllUsersForEvent([FromQuery] int eventId)
        {
            var getAllUsersForEventQuery = new GetAllUsersForEventQuery { EventId = eventId};
            
            return Ok(await Mediator.Send(getAllUsersForEventQuery));
        }

        [HttpGet("userEvents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllEventsForUser()
        {
            var getAllEventsForUserQuery = new GetAllEventsForUserQuery { };
            
            return Ok(await Mediator.Send(getAllEventsForUserQuery));
        }

        [HttpPost("RegisterUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RegisterUserToEvent(int eventId)
        {
            var registerUserToEvent = new RegisterUserToEventCommand { EventId = eventId};
            await Mediator.Send(registerUserToEvent);
            return Ok();
        }

        [HttpPost("CancelUserRegistration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CancelUserRegistrationFromEvent(int eventId)
        {
            var cancelUserRegistrationFromEvent = new CancelUserRegistrationFromEventCommand  { EventId = eventId };
            await Mediator.Send(cancelUserRegistrationFromEvent);
            return Ok();
        }

    }
}
