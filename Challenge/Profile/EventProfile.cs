using AutoMapper;
using Challenge.DTOs;
using Challenge.Entities;

namespace Practice.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();
        }
    }
}
