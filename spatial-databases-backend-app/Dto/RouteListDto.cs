namespace spatial_databases_backend_app.Dto
{
    public class RouteListDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string? Description { get; init; }
        public int DurationHours { get; init; }
        public string Difficulty { get; init; }
    }
}
