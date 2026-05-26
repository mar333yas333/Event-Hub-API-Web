using Event_hub_back_end.Models;

namespace Event_hub_back_end.DTOs
{
    public class CreateEventDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public EventStatus Status { get; set; }
    }
}