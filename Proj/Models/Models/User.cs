using System.ComponentModel.DataAnnotations;

namespace Event_hub_back_end.Models
{
    public enum UserRole { Admin, Speaker, Attendee }

    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(100)]
        public string FullName { get; set; } = null!;

        [Required,EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;


        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Event> OrganizedEvents { get; set; } = new();
        public List<Session> Sessions { get; set; } = new();
        public List<Registration> Registrations { get; set; } = new();
        public List<Notification> Notifications { get; set; } = new();
    }
}