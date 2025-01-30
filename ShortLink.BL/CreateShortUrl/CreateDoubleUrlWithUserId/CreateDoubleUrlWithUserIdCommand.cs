using MediatR;

namespace ShortLink.BL.CreateShortUrl.CreateDoubleUrlWithUserId
{
    public record CreateDoubleUrlWithUserIdCommand : IRequest<string>
    {
        public string OriginalUrl { get; set; }
        public Guid UserId { get; set; }
    }
}
