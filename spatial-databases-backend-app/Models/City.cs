using NetTopologySuite.Geometries;

namespace spatial_databases_backend_app.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public Point Location { get; set; } = default!;
        public string Timezone { get; set; } = "Europe/Moscow";

        // Навигация
        public ICollection<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<WeatherForecast> WeatherForecasts { get; set; } = new List<WeatherForecast>();
        public ICollection<Route> Routes { get; set; } = new List<Route>();
    }
}
