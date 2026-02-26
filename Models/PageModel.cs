namespace ManvarFitness.Models
{
    public class PageModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public List<PageModel> SubPages { get; set; } = new List<PageModel>();
    }
}
