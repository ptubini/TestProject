namespace TestProject.Application.Models
{
    public class ErrorDetails 
    {
        public string Title { get; set; }
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
        public int Status { get; set; }
        public string Type { get; set; }
    }
}
