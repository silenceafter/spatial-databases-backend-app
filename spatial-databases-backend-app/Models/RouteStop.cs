using System.ComponentModel.DataAnnotations;

namespace spatial_databases_backend_app.Models
{
    public class RouteStop
    {
        public int Id { get; set; }
        public int RouteId { get; set; }
        public int PointOfInterestId { get; set; }
        public int OrderIndex { get; set; }
        public string? Note { get; set; }

        // Навигация
        public Route Route { get; set; } = default!;
        public PointOfInterest PointOfInterest { get; set; } = default!;
    }
}
