using MediatR;

namespace ShortLink.BL.User.DeleteUserById
{
    public record DeleteUserByIdCommand: IRequest
    {
        public Guid Id { get; set; }
    }
}
