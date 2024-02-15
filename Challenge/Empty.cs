
//Event.cs
// EventDto.cs
//using AutoMapper;
//using Challenge.Database;
//using Challenge.DTOs;
//using Challenge.Entities;
//using log4net;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System;

//namespace Challenge.DTOs
//{
//    public class EventDto
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public string Description { get; set; }
//        public DateTime Date { get; set; }
//        public string Location { get; set; }
//        public int MaxAttendees { get; set; }
//        public int RegistrationFee { get; set; }
//    }
//}
//EventDto.cs
//using System;

//namespace Challenge.DTOs
//{
//    public class EventDto
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public string Description { get; set; }
//        public DateTime Date { get; set; }
//        public string Location { get; set; }
//        public int MaxAttendees { get; set; }
//        public int RegistrationFee { get; set; }
//    }
//}
//EventController.cs
//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Challenge.DTOs;
//using Challenge.Service;
//using Challenge.Entities;
//using Challenge.Model;
//using log4net;
//using System;
//using System.Collections.Generic;

//namespace Practice.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EventController : ControllerBase
//    {
//        private readonly IEventService _eventService;
//        private readonly IMapper _mapper;
//        private readonly ILog _logger;
//        private readonly IConfiguration _configuration;

//        public EventController(IEventService eventService, IMapper mapper, ILog logger, IConfiguration configuration)
//        {
//            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
//            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
//            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
//            _configuration = configuration;
//        }

//        [HttpGet("GetAllEvents")]
//        [Authorize(Roles = "Admin, User")]
//        public IActionResult GetAllEvents()
//        {
//            try
//            {
//                List<Event> events = _eventService.GetAllEvents();
//                List<EventDto> eventsDto = _mapper.Map<List<EventDto>>(events);
//                _logger.Info("Retrieved all events successfully.");
//                return Ok(eventsDto);
//            }
//            catch (Exception ex)
//            {
//                _logger.Error($"Error getting all events: {ex.Message}");
//                return StatusCode(500, ex.Message);
//            }
//        }

//        [HttpGet("{eventId}")]
//        [Authorize(Roles = "Admin, User")]
//        public ActionResult<EventDto> GetEventById(int eventId)
//        {
//            try
//            {
//                var ev = _eventService.GetEventById(eventId);

//                if (ev == null)
//                {
//                    return NotFound();
//                }

//                var eventDTO = _mapper.Map<EventDto>(ev);
//                return Ok(eventDTO);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal Server Error: {ex.Message}");
//            }
//        }

//        [HttpPost("CreateEvent")]
//        [Authorize(Roles = "Admin")]
//        public IActionResult CreateEvent(EventDto eventDto)
//        {
//            try
//            {
//                Event ev = _mapper.Map<Event>(eventDto);
//                _eventService.CreateEvent(ev);
//                _logger.Info($"Event created successfully. Event ID: {ev.Id}");
//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                _logger.Error($"Error creating event: {ex.Message}");
//                return StatusCode(500, ex.Message);
//            }
//        }

//        [HttpPut("UpdateEvent/{eventId}")]
//        [Authorize(Roles = "Admin")]
//        public IActionResult UpdateEvent(int eventId, EventDto eventDto)
//        {
//            try
//            {
//                Event ev = _mapper.Map<Event>(eventDto);
//                _eventService.UpdateEvent(eventId, ev);
//                _logger.Info($"Event updated successfully. Event ID: {eventId}");
//                return Ok(ev);
//            }
//            catch (Exception ex)
//            {
//                _logger.Error($"Error updating event: {ex.Message}");
//                return StatusCode(500, ex.Message);
//            }
//        }

//        [HttpDelete("DeleteEvent/{eventId}")]
//        [Authorize(Roles = "Admin")]
//        public IActionResult DeleteEvent(int eventId)
//        {
//            try
//            {
//                _eventService.DeleteEvent(eventId);
//                _logger.Info($"Event deleted successfully. Event ID: {eventId}");
//                return StatusCode(200);
//            }
//            catch (Exception ex)
//            {
//                _logger.Error($"Error deleting event: {ex.Message}");
//                return StatusCode(500, ex.Message);
//            }
//        }
//    }
//}
//IEventService.cs
//using System.Collections.Generic;

//public interface IEventService
//{
//    List<Event> GetAllEvents();
//    Event GetEventById(int eventId);
//    void CreateEvent(Event newEvent);
//    void UpdateEvent(int eventId, Event updatedEvent);
//    void DeleteEvent(int eventId);
//}
//using Challenge.Database;
//using Challenge.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Challenge.Service
//{
//    public class EventService : IEventService
//    {
//        private readonly MyContext _context;

//        public EventService(MyContext context)
//        {
//            _context = context ?? throw new ArgumentNullException(nameof(context));
//        }

//        public void CreateEvent(Event newEvent)
//        {
//            try
//            {
//                _context.Events.Add(newEvent);
//                _context.SaveChanges();
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

//        public void DeleteEvent(int eventId)
//        {
//            Event ev = _context.Events.Find(eventId);
//            if (ev != null)
//            {
//                _context.Events.Remove(ev);
//                _context.SaveChanges();
//            }
//        }

//        public void UpdateEvent(int eventId, Event updatedEvent)
//        {
//            Event existingEvent = _context.Events.Find(eventId);
//            if (existingEvent != null)
//            {
//                existingEvent.Title = updatedEvent.Title;
//                existingEvent.Description = updatedEvent.Description;
//                existingEvent.Date = updatedEvent.Date;
//                existingEvent.Location = updatedEvent.Location;
//                existingEvent.MaxAttendees = updatedEvent.MaxAttendees;
//                existingEvent.RegistrationFee = updatedEvent.RegistrationFee;

//                _context.SaveChanges();
//            }
//        }

//        public List<Event> GetAllEvents()
//        {
//            return _context.Events.ToList();
//        }

//        public Event GetEventById(int eventId)
//        {
//            return _context.Events.Find(eventId);
//        }
//    }
//}
