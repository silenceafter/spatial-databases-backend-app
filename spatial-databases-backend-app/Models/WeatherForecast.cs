using System.ComponentModel.DataAnnotations;

namespace spatial_databases_backend_app.Models
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public DateOnly ForecastDate { get; set; }
        public int TemperatureMin { get; set; }
        public int TemperatureMax { get; set; }
        public string Condition { get; set; } = default!;
        public int? Humidity { get; set; }
        public decimal? WindSpeed { get; set; }

        // Навигация
        public City City { get; set; } = default!;
    }
}
