using MediatR;
using Microsoft.EntityFrameworkCore;
using ShortLink.DAL.Data;

namespace ShortLink.BL.DoubleUrl.GetAllDoubleUrls
{
    public class GetAllDoubleUrlsQueryHandler : IRequestHandler<GetAllDoubleUrlsQuery, List<Domain.Entities.DoubleUrl>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllDoubleUrlsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.DoubleUrl>> Handle(GetAllDoubleUrlsQuery request, CancellationToken cancellationToken)
        {
            return await _context.DoubleUrls
            .Select(d => new Domain.Entities.DoubleUrl
            {
                Id = d.Id,
                OriginalUrl = d.OriginalUrl,
                ShortUrl = d.ShortUrl,
                UserId = d.UserId,
            }).ToListAsync(cancellationToken);
        }
    }
}
