using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.NetTopologySuite;
using spatial_databases_backend_app.Dto;

namespace spatial_databases_backend_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PointsOfInterestController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<ActionResult<List<POIDto>>> GetPoiByCategories(
            [FromQuery] string[] categories,
            [FromQuery] int limit = 10,
            [FromQuery] int cityId = 1)
        {
            var query = _db.PointsOfInterest.AsQueryable();

            if (categories?.Length > 0)
                query = query.Where(p => categories.Contains(p.Category));

            query = query.Where(p => p.CityId == cityId);

            // Загружаем в память
            var pois = await query.ToListAsync();

            // Маппим уже в памяти
            var result = pois
                .OrderBy(x => Guid.NewGuid()) // случайный порядок
                .Take(limit)
                .Select(p => new POIDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Latitude = p.Location.Y,
                    Longitude = p.Location.X,
                    Category = p.Category ?? "other"/*,
                    Address = p.Address,
                    OpeningHours = p.OpeningHours*/
                })
                .ToList();

            return Ok(result);
        }
    }
}
