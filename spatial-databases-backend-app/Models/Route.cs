using NetTopologySuite.Geometries;

namespace spatial_databases_backend_app.Models
{
    public class Route
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public int DurationHours { get; set; }
        public string? Difficulty { get; set; }
        public bool IsPublic { get; set; }
        public LineString? Path { get; set; }

        // Навигация
        public City City { get; set; } = default!;
        public ICollection<RouteStop> Stops { get; set; } = new List<RouteStop>();
    }
}
