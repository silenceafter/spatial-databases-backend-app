using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.NetTopologySuite;
using spatial_databases_backend_app.Dto;

namespace spatial_databases_backend_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CitiesController(ApplicationDbContext db) => _db = db;

        /// <summary>
        /// Получить список городов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<CityDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CityDto>>> Get()
        { 
            var citiesFromDb = await _db.Cities.ToListAsync();
            var dto = citiesFromDb.Select(c => new CityDto
            {
                Id = c.Id,
                Name = c.Name,
                Latitude = c.Location.Y,
                Longitude = c.Location.X
            }).ToList();
            return Ok(dto);
        }

        /// <summary>
        /// Получить город с достопримечательностями
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CityDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CityDetailDto>> GetById(int id)
        {
            var city = await _db.Cities
                .Include(c => c.PointsOfInterest)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (city == null)
                return NotFound();

            var dto = new CityDetailDto
            {
                Id = city.Id,
                Name = city.Name,
                POIs = city.PointsOfInterest.Select(p => new POIDto
                {
                    Name = p.Name,
                    Latitude = p.Location.Y,
                    Longitude = p.Location.X
                }).ToList()
            };

            return Ok(dto);
        }
    }
}
