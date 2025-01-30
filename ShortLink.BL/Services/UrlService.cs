namespace ShortLink.BL.Services
{
    public class UrlService
    {
        public string GenerateShortUrl()
        {
            var guid = Guid.NewGuid().ToString("N");
            return guid.Substring(0, 8);
        }
    }
}
