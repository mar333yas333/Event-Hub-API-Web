using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHubBackend.Models
{
    public class Session
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid EventId { get; set; }

        public Event Event { get; set; } = null!;

        [Required]
        public Guid SpeakerId { get; set; }

        public User Speaker { get; set; } = null!;

        [Required]
        public string Title { get; set; } = null!;

        public string? Abstract { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        public int RegisteredCount { get; set; } = 0;

        [Required]
        public string Room { get; set; } = null!;

        public ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}