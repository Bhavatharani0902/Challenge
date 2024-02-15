// DTOs/EventDto.cs
using Challenge.Database;

public class EventDto
{
    public int EventId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public int MaxAttendees { get; set; }
    public int RegistrationFee { get; set; }
}


