using MediatR;

namespace ShortLink.BL.DoubleUrl.GetOriginalUrl
{
    public record GetOriginalUrlCommand : IRequest<string>
    {
        public string ShortUrl { get; set; }
    }
}
