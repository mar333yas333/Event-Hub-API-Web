using Event_hub_back_end.Data;
using Event_hub_back_end.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Event_hub_back_end.DTOs;

namespace Event_hub_back_end.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var events = await _context.Events
                .Where(e => e.Status == EventStatus.Published)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new
                {
                    e.Id,
                    e.Title,
                    e.Location,
                    e.StartDate,
                    e.EndDate,
                    e.Status
                })
                .ToListAsync();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(Guid id)
        {
            var ev = await _context.Events
                .Include(e => e.Sessions)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (ev == null) return NotFound();

            return Ok(ev);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto dto)
        {
            if (dto.EndDate <= dto.StartDate)
                return BadRequest("EndDate must be after StartDate");

            var organizerId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            var ev = new Event
            {
                Id = Guid.NewGuid(),
                OrganizerId = organizerId,
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = EventStatus.Draft
            };

            _context.Events.Add(ev);
            await _context.SaveChangesAsync();

            return Ok(ev);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] CreateEventDto dto)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();

            ev.Title = dto.Title;
            ev.Description = dto.Description;
            ev.Location = dto.Location;
            ev.StartDate = dto.StartDate;
            ev.EndDate = dto.EndDate;
            ev.Status = dto.Status;

            await _context.SaveChangesAsync();
            return Ok(ev);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return NotFound();

            ev.Status = EventStatus.Cancelled;
            await _context.SaveChangesAsync();

            return Ok("Event cancelled");
        }
    }
}