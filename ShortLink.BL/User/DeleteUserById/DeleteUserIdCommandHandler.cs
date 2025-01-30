using MediatR;
using ShortLink.DAL.Data;

namespace ShortLink.BL.User.DeleteUserById
{
    internal class DeleteUserIdCommandHandler : IRequestHandler<DeleteUserByIdCommand>
    {
        private readonly ApplicationDbContext _context;
        public DeleteUserIdCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id, cancellationToken);
            Guard.AgainstNull(user, nameof(user));

            _context.Users.Remove(user);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
