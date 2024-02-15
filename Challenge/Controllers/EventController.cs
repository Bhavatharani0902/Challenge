using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using log4net;
using Microsoft.Extensions.Configuration;
using Challenge.DTOs;
using Challenge.Service;
using Challenge.Entities;
using System;
using System.Collections.Generic;

namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        private readonly ILog _logger;
        private readonly IConfiguration _configuration;

        public EventController(IEventService eventService, IMapper mapper, ILog logger, IConfiguration configuration)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration;
        }

        [HttpGet("GetAllEvents")]
        [AllowAnonymous]
        public IActionResult GetAllEvents()
        {
            try
            {
                List<Event> events = _eventService.GetAllEvents();
                List<EventDto> eventsDto = _mapper.Map<List<EventDto>>(events);
                _logger.Info("Retrieved all events successfully.");
                return Ok(eventsDto);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error getting all events: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{eventId}")]
        [AllowAnonymous]
        public ActionResult<EventDto> GetEventById(int eventId)
        {
            try
            {
                var evnt = _eventService.GetEventById(eventId);

                if (evnt == null)
                {
                    return NotFound();
                }

                var eventDTO = _mapper.Map<EventDto>(evnt);
                return Ok(eventDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("CreateEvent")]
        //[Authorize(Roles = "Admin")]
        public IActionResult CreateEvent(EventDto eventDto)
        {
            try
            {
                Event evnt = _mapper.Map<Event>(eventDto);
                _eventService.CreateEvent(evnt);
                _logger.Info($"Event created successfully. Event ID: {evnt.EventId}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error creating event: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            try
            {
                var existingEvent = _eventService.GetEventById(id);

                if (existingEvent == null)
                {
                    _logger?.Warn($"Event with ID {id} not found.");
                    return NotFound("Event not found");
                }

                existingEvent.Title = updatedEvent.Title;
                existingEvent.Description = updatedEvent.Description;
                existingEvent.Date = updatedEvent.Date;
                existingEvent.Location = updatedEvent.Location;
                existingEvent.MaxAttendees = updatedEvent.MaxAttendees;
                existingEvent.RegistrationFee = updatedEvent.RegistrationFee;

                _eventService.UpdateEvent(existingEvent);

                _logger?.Info($"Event updated successfully. Event ID: {id}");
                return Ok(existingEvent);
            }
            catch (Exception ex)
            {
                _logger?.Error($"Error in UpdateEvent: {ex.Message}");
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpDelete("DeleteEvent/{id}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                _eventService.DeleteEvent(id);
                _logger.Info($"Event deleted successfully. Event ID: {id}");
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error deleting event: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("SearchEventByTitle")]
        [AllowAnonymous]
        public IActionResult SearchEventByTitle(string eventTitle)
        {
            try
            {
                List<Event> matchingEvents = _eventService.SearchEventByTitle(eventTitle);

                if (matchingEvents.Count == 0)
                {
                    return NotFound();
                }

                List<EventDto> matchingEventsDto = _mapper.Map<List<EventDto>>(matchingEvents);
                return Ok(matchingEventsDto);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error searching events by title: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
