using NetTopologySuite.Geometries;

namespace spatial_databases_backend_app.Models
{
    public class PointOfInterest
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public Point Location { get; set; } = default!;
        public string? Category { get; set; }
        public string? Address { get; set; }
        public string? OpeningHours { get; set; }
        public string? PhotoUrl { get; set; }

        // Навигация
        public City City { get; set; } = default!;
        public ICollection<RouteStop> RouteStops { get; set; } = new List<RouteStop>();
    }
}
