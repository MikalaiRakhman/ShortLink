namespace ShortLink.Domain.Entities
{
    public class DoubleUrl
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public Guid? UserId { get; set; }
    }
}
