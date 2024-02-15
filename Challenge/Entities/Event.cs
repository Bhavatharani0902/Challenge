using AutoMapper;
using Challenge.DTOs;
using Challenge.Entities;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Entities
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int MaxAttendees { get; set; }
        public int RegistrationFee { get; set; }
    }
}


