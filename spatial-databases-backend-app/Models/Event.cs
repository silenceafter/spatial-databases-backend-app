using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace spatial_databases_backend_app.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public Point Location { get; set; } = default!;
        public string? Category { get; set; }
        public string? TicketUrl { get; set; }
        public bool IsFree { get; set; }

        // Навигация
        public City City { get; set; } = default!;
    }
}
