using Challenge.Database;
using Challenge.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Service
{
    public class EventService : IEventService
    {
        private readonly MyContext _context;

        public EventService(MyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Event> GetAllEvents()
        {
            return _context.Events.ToList();
        }

        public Event GetEventById(int eventId)
        {
            return _context.Events.Find(eventId);
        }

        public void CreateEvent(Event newEvent)
        {
            try
            {
                _context.Events.Add(newEvent);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateEvent(Event updatedEvent)
        {
            _context.Events.Update(updatedEvent);
            _context.SaveChanges();
        }

        public void DeleteEvent(int eventId)
        {
            Event existingEvent = _context.Events.Find(eventId);
            if (existingEvent != null)
            {
                _context.Events.Remove(existingEvent);
                _context.SaveChanges();
            }
        }

        public List<Event> SearchEventByTitle(string eventTitle)
        {
            // Case-insensitive search for events by title
            return _context.Events
                .Where(e => e.Title.ToLower().Contains(eventTitle.ToLower()))
                .ToList();
        }
    }
}
