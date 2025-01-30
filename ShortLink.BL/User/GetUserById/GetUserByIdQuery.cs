using MediatR;
using ShortLink.BL.Models;

namespace ShortLink.BL.User.GetUserById
{
    public record GetUserByIdQuery: IRequest<UserDTO>
    {
        public Guid Id { get; set; }
    }
}
