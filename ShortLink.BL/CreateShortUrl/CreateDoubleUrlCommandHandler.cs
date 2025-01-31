using MediatR;
using ShortLink.DAL.Data;


namespace ShortLink.BL.CreateShortUrl
{
    public class CreateDoubleUrlCommandHandler: IRequestHandler<CreateDoubleUrlCommand, string>
    {
        private ApplicationDbContext _context;
        public CreateDoubleUrlCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateDoubleUrlCommand request, CancellationToken cancellationToken)
        {
            var shortUrl = GenerateShortUrl();
            var doubleUrl = new Domain.Entities.DoubleUrl
            {
                OriginalUrl = request.OriginalUrl,
                ShortUrl = shortUrl
            };

            _context.DoubleUrls.Add(doubleUrl);

            await _context.SaveChangesAsync();

            return shortUrl;
        }

        private string GenerateShortUrl()
        {
            var guid = Guid.NewGuid().ToString("N");  
            return guid.Substring(0, 8);              
        }
    }
}
