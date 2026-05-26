using Event_hub_back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_hub_back_end.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UNIQUE EMAIL
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // EVENT → ORGANIZER
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(u => u.OrganizedEvents)
                .HasForeignKey(e => e.OrganizerId)
                .OnDelete(DeleteBehavior.Restrict);

            // SESSION → EVENT (cascade delete)
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Event)
                .WithMany(e => e.Sessions)
                .HasForeignKey(s => s.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // SESSION → SPEAKER
            modelBuilder.Entity<Session>()
                .HasOne(s => s.Speaker)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.SpeakerId)
                .OnDelete(DeleteBehavior.Restrict);

            // REGISTRATION → USER
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.User)
                .WithMany(u => u.Registrations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // REGISTRATION → SESSION
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Session)
                .WithMany(s => s.Registrations)
                .HasForeignKey(r => r.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // NOTIFICATION → USER
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // EVENT: StartDate must be before EndDate
            modelBuilder.Entity<Event>()
                .HasCheckConstraint("CK_Event_DateRange", "[EndDate] > [StartDate]");

            // SESSION: Capacity must be > 0
            modelBuilder.Entity<Session>()
                .HasCheckConstraint("CK_Session_Capacity", "[Capacity] > 0");
        }
    }
}