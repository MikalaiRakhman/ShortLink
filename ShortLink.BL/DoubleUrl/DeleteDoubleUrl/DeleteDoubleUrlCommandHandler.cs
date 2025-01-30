using MediatR;
using ShortLink.DAL.Data;

namespace ShortLink.BL.DoubleUrl.DeleteDoubleUrl
{
    public class DeleteDoubleUrlCommandHandler : IRequestHandler<DeleteDoubleUrlCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteDoubleUrlCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteDoubleUrlCommand request, CancellationToken cancellationToken)
        {
            var doubleUrl = await _context.DoubleUrls.FindAsync(request.Id, cancellationToken);
            Guard.AgainstNull(doubleUrl, nameof(doubleUrl));

            _context.DoubleUrls.Remove(doubleUrl);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
