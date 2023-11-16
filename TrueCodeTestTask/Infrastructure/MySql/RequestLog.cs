namespace TrueCodeTestTask.Infrastructure.MySql
{
    public class RequestLog
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public DateTimeOffset Date { get; set; }
        public string? Response { get; set; }
    }
}
