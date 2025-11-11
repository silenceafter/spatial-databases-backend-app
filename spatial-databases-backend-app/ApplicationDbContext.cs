using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using spatial_databases_backend_app.Models;
using Route = spatial_databases_backend_app.Models.Route;

namespace spatial_databases_backend_app
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteStop> RouteStops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка PostGIS
            modelBuilder.HasPostgresExtension("postgis");

            // Геометрия: POINT и LINESTRING
            modelBuilder.Entity<City>(eb =>
            {
                eb.Property(c => c.Location).HasColumnType("geography (point, 4326)");
            });

            modelBuilder.Entity<PointOfInterest>(eb =>
            {
                eb.Property(p => p.Location).HasColumnType("geography (point, 4326)");
            });

            modelBuilder.Entity<Event>(eb =>
            {
                eb.Property(e => e.Location).HasColumnType("geography (point, 4326)");
            });

            modelBuilder.Entity<Route>(eb =>
            {
                eb.Property(r => r.Path).HasColumnType("geography (linestring, 4326)");
            });

            // Отношения
            modelBuilder.Entity<PointOfInterest>()
                .HasOne(p => p.City)
                .WithMany(c => c.PointsOfInterest)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.City)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WeatherForecast>()
                .HasOne(w => w.City)
                .WithMany(c => c.WeatherForecasts)
                .HasForeignKey(w => w.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Route>()
                .HasOne(r => r.City)
                .WithMany(c => c.Routes)
                .HasForeignKey(r => r.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RouteStop>()
                .HasOne(rs => rs.Route)
                .WithMany(r => r.Stops)
                .HasForeignKey(rs => rs.RouteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RouteStop>()
                .HasOne(rs => rs.PointOfInterest)
                .WithMany(p => p.RouteStops)
                .HasForeignKey(rs => rs.PointOfInterestId)
                .OnDelete(DeleteBehavior.Restrict); // чтобы не удалился POI при удалении остановки

            // Уникальный индекс для прогноза погоды
            modelBuilder.Entity<WeatherForecast>()
                .HasIndex(w => new { w.CityId, w.ForecastDate })
                .IsUnique();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Если не используешь DI (обычно не нужно в ASP.NET Core)
            // optionsBuilder.UseNpgsql("...", o => o.UseNetTopologySuite());
        }
    }
}
