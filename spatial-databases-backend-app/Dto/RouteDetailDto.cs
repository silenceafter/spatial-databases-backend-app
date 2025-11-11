namespace spatial_databases_backend_app.Dto
{
    public class RouteDetailDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string? Description { get; init; }
        public int DurationHours { get; init; }
        public double[][] Path { get; init; }  // [[lon, lat], [lon, lat], ...]
        public List<RouteStopDto> Stops { get; init; } = new();
    }
}
