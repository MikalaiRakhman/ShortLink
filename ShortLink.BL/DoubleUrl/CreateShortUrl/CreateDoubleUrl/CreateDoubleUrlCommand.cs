using MediatR;

namespace ShortLink.BL.DoubleUrl.CreateShortUrl.CreateDoubleUrl
{
    public record CreateDoubleUrlCommand : IRequest<string>
    {
        public string OriginalUrl { get; set; }
    }
}
