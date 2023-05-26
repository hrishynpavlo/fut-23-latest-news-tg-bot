namespace FUT_23_LATEST_NEWS.Infrastructure.Models
{
    public class FutContent
    {
        public string Name { get; set; }
        public FutContentType Type { get; set; }
        public List<FutContentItem> ContentItems { get; set; }
    }

    public class FutContentItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
