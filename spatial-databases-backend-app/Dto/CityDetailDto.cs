namespace spatial_databases_backend_app.Dto
{
    public record CityDetailDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<POIDto> POIs { get; init; } = new();
    }
}
