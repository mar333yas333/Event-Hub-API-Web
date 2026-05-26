using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHubBackend.Models
{
    public class Registration
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        [Required]
        public Guid SessionId { get; set; }

        public Session Session { get; set; } = null!;

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public bool IsCancelled { get; set; } = false;
    }
}