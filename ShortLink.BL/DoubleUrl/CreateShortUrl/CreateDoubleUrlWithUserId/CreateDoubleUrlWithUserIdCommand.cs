using MediatR;

namespace ShortLink.BL.DoubleUrl.CreateShortUrl.CreateDoubleUrlWithUserId
{
    public record CreateDoubleUrlWithUserIdCommand : IRequest<string>
    {
        public string OriginalUrl { get; set; }
        public Guid UserId { get; set; }
    }
}
