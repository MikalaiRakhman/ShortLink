using MediatR;

namespace ShortLink.BL.GetOriginalUrl
{
    public record GetOriginalUrlCommand: IRequest<string>
    {
        public string ShortUrl  { get; set; }
    }
}
