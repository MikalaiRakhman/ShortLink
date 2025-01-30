using MediatR;

namespace ShortLink.BL.DeleteDoubleUrl
{
    public record DeleteDoubleUrlCommand: IRequest
    {
        public Guid Id { get; set; }
    }
}