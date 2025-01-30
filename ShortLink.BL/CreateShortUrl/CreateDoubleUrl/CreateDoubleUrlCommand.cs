using MediatR;

namespace ShortLink.BL.CreateShortUrl.CreateDoubleUrl
{
    public record CreateDoubleUrlCommand: IRequest<string>
    {
        public string OriginalUrl { get; set; }
    }
}
