using MediatR;
using ShortLink.BL.Services;
using ShortLink.DAL.Data;
using System.Data;

namespace ShortLink.BL.DoubleUrl.CreateShortUrl.CreateDoubleUrl
{
    public class CreateDoubleUrlCommandHandler : IRequestHandler<CreateDoubleUrlCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly UrlService _urlService;
        public CreateDoubleUrlCommandHandler(ApplicationDbContext context, UrlService urlService)
        {
            _context = context;
            _urlService = urlService;
        }
        public async Task<string> Handle(CreateDoubleUrlCommand request, CancellationToken cancellationToken)
        {
            if (_urlService.IsValidUrl(request.OriginalUrl)) 
            {
                var shortUrl = _urlService.GenerateShortUrl();
                var doubleUrl = new Domain.Entities.DoubleUrl
                {
                    OriginalUrl = request.OriginalUrl,
                    ShortUrl = shortUrl,
                };

                _context.DoubleUrls.Add(doubleUrl);

                await _context.SaveChangesAsync();

                return shortUrl;
            } 
            else
            {
                throw new Exception("Url is not valid.");
            }

            
        }
    }
}
