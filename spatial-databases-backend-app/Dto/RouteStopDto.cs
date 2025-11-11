namespace spatial_databases_backend_app.Dto
{
    public class RouteStopDto
    {
        public int OrderIndex { get; init; }
        public string? Note { get; init; }
        public POIDto PointOfInterest { get; init; }
    }
}
