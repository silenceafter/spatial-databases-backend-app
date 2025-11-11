namespace spatial_databases_backend_app.Dto
{
    public record CityDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
    }
}
