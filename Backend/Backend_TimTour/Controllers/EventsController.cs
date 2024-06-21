using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Interfaces.Parsers;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.SimplifiedLocations;
using Backend_TimTour.Models.RequestModels;
using Backend_TimTour.Models.ResultsModels;
using Backend_TimTour.Parsers;
using Backend_TimTour.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_TimTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventParser _eventParser;
        public EventsController(IEventRepository eventRepository, IEventParser eventParser)
        {
            this._eventRepository = eventRepository;
            this._eventParser = eventParser;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SimplifiedEvent>>> GetEventsAsync()
        {
            try
            {
                var events = await _eventRepository.GetAllEventsAsync();
                IEnumerable<RequestEvent> transformedEvents = events
                    .Select(_eventParser.ParseEventToDatabaseObject)
                    .ToList();
                return Ok(transformedEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpGet("simplifiedEvent")]
        public async Task<ActionResult<IEnumerable<SimplifiedEvent>>> GetSimplifiedEventsAsync()
        {
            try
            {
                var events = await _eventRepository.GetSimplifiedEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpGet("pendingEvent")]
        public async Task<ActionResult<IEnumerable<SimplifiedEvent>>> GetPendingEventsAsync()
        {
            try
            {
                var events = await _eventRepository.GetPendingEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpPost]
        public async Task<ActionResult<RepositoryResult>> InsertEventAsync([FromBody] AddEventRequestModel Request)
        {
            var eventToParse = Request.eventAdded;
            try
            {
                var (eventToInsert, result) = await _eventParser.ParseRequestToEvent(eventToParse);
                if (result)
                {
                    eventToInsert.Status = "pending";
                    var insertionResult = await _eventRepository.AddEventAsync(eventToInsert);
                    return Ok(new { result = insertionResult.ToString() });
                }
                else
                {
                    return StatusCode(500, new { result = "Internal server error: " + "Parsing Failed" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
    }
}
