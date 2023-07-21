using EventHub.Dal;
using EventHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        //--dependency injection via controller
        private readonly ICommonRepository<Event> _eventRepository;

        //--constructor
        public EventsController(ICommonRepository<Event> repository)
        {
            _eventRepository = repository;
        }

        //--get all events
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Event>> Get()
        {
            var events = _eventRepository.GetAll();

            return events == null ? NotFound() : Ok(events);
        }

        //--get 1 event
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Event> GetDetails(int id)
        {
            var events = _eventRepository.GetDetails(id);

            return events == null ? NotFound() : Ok(events);
        }

        //--create event
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Event> Create(Event events)
        {
            _eventRepository.Insert(events);
            var result = _eventRepository.SaveChanges();

            if (result > 0)
            {
                return CreatedAtAction("GetDetails", new { id = events.EventId }, events);
            }

            return BadRequest();
        }

        //--update event
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Event> Update(Event events)
        {
            _eventRepository.Update(events);
            var result = _eventRepository.SaveChanges();

            if (result > 0)
            {
                return NoContent();
            }

            return BadRequest();
        }

        //--delete event
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Event> Delete(int id)
        {
            var events = _eventRepository.GetDetails(id);

            if(events == null)
            {
                return NotFound();
            }
            else
            {
                _eventRepository.Delete(events);
                _eventRepository.SaveChanges();

                return NoContent();
            }



        }
     

    }
}
