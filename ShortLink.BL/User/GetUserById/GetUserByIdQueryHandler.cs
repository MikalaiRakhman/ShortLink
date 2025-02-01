using MediatR;
using ShortLink.BL.Models;
using ShortLink.DAL.Data;

namespace ShortLink.BL.User.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly ApplicationDbContext _context;
        public GetUserByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id);

            Guard.AgainstNull(user, nameof(user));

            return new UserDTO
            {
                Id = user.Id,                
                Email = user.Email
            };
        }
    }
}
