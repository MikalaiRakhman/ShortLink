using MediatR;
using ShortLink.BL.Models;

namespace ShortLink.BL.User.GetAllUsers
{
    public record GetAllUsersQuery: IRequest<List<UserDTO>>
    {
    }
}
