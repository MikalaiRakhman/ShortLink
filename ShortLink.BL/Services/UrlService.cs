using ShortLink.BL.Interfaces;
using System.Text.RegularExpressions;

namespace ShortLink.BL.Services
{
    public class UrlService: IUrlService
    {
        private static readonly Regex UrlPattern = new Regex(
         @"^(https?:\/\/)([a-zA-Z0-9.-]+)+(:\d+)?(\/[\w/_.-]*)?(\?[^\s]*)?(#[^\s]*)?$",
         RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public string GenerateShortUrl()
        {
            var guid = Guid.NewGuid().ToString("N");
            return guid.Substring(0, 8);
        }        

        public bool IsValidUrl(string url)
        {
            return UrlPattern.IsMatch(url);
        }
    }
}
