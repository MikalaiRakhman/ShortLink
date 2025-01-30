using MediatR;

namespace ShortLink.BL.CreateShortUrl
{
    public record CreateDoubleUrlCommand: IRequest<string>
    {
        public string OriginalUrl {  get; set; } 
    }
}
