namespace spatial_databases_backend_app.Dto
{
    public class POIDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public string Category { get; init; }
    }
}
