using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortLink.DAL.Data;

namespace ShortLink.BL.DoubleUrl.GetOriginalUrl
{
    public class GetOriginalUrlCommandHandler : IRequestHandler<GetOriginalUrlCommand, string>
    {
        private readonly ApplicationDbContext _context;
        public GetOriginalUrlCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetOriginalUrlCommand request, CancellationToken cancellationToken)
        {
            var originalUrl = await _context.DoubleUrls.FirstOrDefaultAsync(u => u.ShortUrl == request.ShortUrl);
            Guard.AgainstNull(originalUrl, nameof(originalUrl));

            return originalUrl.OriginalUrl;
        }
    }
}
