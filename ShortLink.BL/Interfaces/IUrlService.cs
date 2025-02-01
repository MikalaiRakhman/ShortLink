namespace ShortLink.BL.Interfaces
{
    public interface IUrlService
    {
        bool IsValidUrl(string url);
        string GenerateShortUrl();
    }
}
