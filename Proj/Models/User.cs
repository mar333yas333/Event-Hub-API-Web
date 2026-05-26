using System.ComponentModel.DataAnnotations;

namespace EventHubBackend.Models
{
    public enum UserRole { Admin, Speaker, Attendee }

    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Event> OrganizedEvents { get; set; }
        public List<Session> Sessions { get; set; }
        public List<Registration> Registrations { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}