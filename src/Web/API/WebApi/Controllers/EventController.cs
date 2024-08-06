using EventSystem.Application.EventManagement.Commands.CreateEventCommand;
using EventSystem.Application.EventManagement.Queries.GetEventById;
using EventSystem.Application.EventManagement.Queries.GetEventsQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/Event")]
    [Authorize]
    public class EventController : ApiControllerBase
    {
        /// <summary>
        /// Endpoints
        /// 	Create Event
        /// 	Get Event by ID
        /// 	List all Events
        /// </summary>

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var command = new GetAllEventsQuery { };
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> GetEventById(int eventId)
        {
            var @eventQuery  = new GetEventByIdQuery { EventId = eventId };
            return Ok(await Mediator.Send(@eventQuery));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand createEventCommand)
        {
            await Mediator.Send(createEventCommand);
            return Ok();
        }
    }
}
