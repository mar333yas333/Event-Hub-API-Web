using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_hub_back_end.Models
{
    public enum EventStatus
    {
        Draft,
        Published,
        Cancelled
    }

    public class Event
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey(nameof(Organizer))]
        public Guid OrganizerId { get; set; }

        public User Organizer { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public string Location { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public EventStatus Status { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}