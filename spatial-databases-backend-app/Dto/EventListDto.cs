namespace spatial_databases_backend_app.Dto
{
    public class EventListDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string? Description { get; init; }
        public DateTime StartAt { get; init; }
        public DateTime EndAt { get; init; }
        public string Category { get; init; }
        public bool IsFree { get; init; }
    }
}
