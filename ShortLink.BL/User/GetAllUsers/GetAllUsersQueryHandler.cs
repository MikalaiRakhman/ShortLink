using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortLink.BL.Models;
using ShortLink.DAL.Data;

namespace ShortLink.BL.User.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDTO>>
    {
        private readonly ApplicationDbContext _context;
        public GetAllUsersQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDTO>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
            .Select(u => new UserDTO
            {
                Id = u.Id,                
                Email = u.Email
            }).ToListAsync(cancellationToken);
        }
    }
}
