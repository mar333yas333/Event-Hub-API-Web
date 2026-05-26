using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EventHubBackend.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Event> Events { get; set; }
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Speaker,
        Attendee
    }
}



public class User() 
{
    public Guid Id { get;set;  }
    [Required,MaxLength(100)]
    public string FullName { get; set; }
    [Required,]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }

}
