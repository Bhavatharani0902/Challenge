using Challenge.Entities;

namespace Challenge.Service
{
    public interface IEventService
    {
        List<Event> GetAllEvents();
        Event GetEventById(int eventId);
        void CreateEvent(Event newEvent);
        void UpdateEvent(Event updatedEvent);
        void DeleteEvent(int eventId);
        List<Event> SearchEventByTitle(string eventTitle);
    }
}
