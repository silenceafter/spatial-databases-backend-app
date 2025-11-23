using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spatial_databases_backend_app.Dto;

namespace spatial_databases_backend_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public EventsController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<List<EventListDto>>> GetEvents([FromQuery] int cityId)
        {
            var events = await _db.Events
                .Where(e => e.CityId == cityId)
                .Select(e => new EventListDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    StartAt = e.StartAt,
                    EndAt = e.EndAt,
                    Category = e.Category ?? "other",
                    IsFree = e.IsFree
                })
                .ToListAsync();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDetailDto>> GetEventById(int id)
        {
            var @event = await _db.Events
                .FirstOrDefaultAsync(e => e.Id == id);

            if (@event == null) return NotFound();

            return Ok(new EventDetailDto
            {
                Id = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
                StartAt = @event.StartAt,
                EndAt = @event.EndAt,
                Location = new POIDto
                {
                    Name = "Место проведения",
                    Latitude = @event.Location.Y,
                    Longitude = @event.Location.X
                },
                Category = @event.Category ?? "other",
                TicketUrl = @event.TicketUrl,
                IsFree = @event.IsFree
            });
        }
    }
}
