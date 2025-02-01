using MediatR;

namespace ShortLink.BL.DoubleUrl.DeleteDoubleUrl
{
    public record DeleteDoubleUrlCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}