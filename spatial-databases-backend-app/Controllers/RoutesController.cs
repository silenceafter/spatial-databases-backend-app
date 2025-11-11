using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spatial_databases_backend_app.Dto;

namespace spatial_databases_backend_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoutesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public RoutesController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<List<RouteListDto>>> GetRoutes([FromQuery] int cityId)
        {
            var routes = await _db.Routes
                .Where(r => r.CityId == cityId)
                .Select(r => new RouteListDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    DurationHours = r.DurationHours,
                    Difficulty = r.Difficulty ?? "medium"
                })
                .ToListAsync();

            return Ok(routes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RouteDetailDto>> GetRouteById(int id)
        {
            var route = await _db.Routes
                .Include(r => r.Stops)
                    .ThenInclude(rs => rs.PointOfInterest)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (route == null) return NotFound();

            return Ok(new RouteDetailDto
            {
                Id = route.Id,
                Title = route.Title,
                Description = route.Description,
                DurationHours = route.DurationHours,
                // Путь как массив координат: [[lon, lat], [lon, lat], ...]
                Path = route.Path?.Coordinates
                    .Select(c => new[] { c.X, c.Y })
                    .ToArray() ?? new double[0][],
                // Остановки с POI
                Stops = route.Stops.OrderBy(rs => rs.OrderIndex)
                    .Select(rs => new RouteStopDto
                    {
                        OrderIndex = rs.OrderIndex,
                        Note = rs.Note,
                        PointOfInterest = new POIDto
                        {
                            Id = rs.PointOfInterest.Id,
                            Name = rs.PointOfInterest.Name,
                            Latitude = rs.PointOfInterest.Location.Y,
                            Longitude = rs.PointOfInterest.Location.X,
                            Category = rs.PointOfInterest.Category ?? "other"
                        }
                    }).ToList()
            });
        }
    }
}
