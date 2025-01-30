using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShortLink.BL.Services;
using ShortLink.DAL.Data;

namespace ShortLink.BL.DoubleUrl.CreateShortUrl.CreateDoubleUrlWithUserId
{
    public class CreateDoubleUrlWithUserIdCommandHandler : IRequestHandler<CreateDoubleUrlWithUserIdCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Domain.Entities.User> _userManager;
        private readonly UrlService _urlService;
        public CreateDoubleUrlWithUserIdCommandHandler(ApplicationDbContext context, UserManager<Domain.Entities.User> userManager, UrlService urlService)
        {
            _context = context;
            _userManager = userManager;
            _urlService = urlService;
        }

        public async Task<string> Handle(CreateDoubleUrlWithUserIdCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            Guard.AgainstNull(user, nameof(user));

            var shortUrl = _urlService.GenerateShortUrl();
            var doubleUrl = new Domain.Entities.DoubleUrl
            {
                OriginalUrl = request.OriginalUrl,
                ShortUrl = shortUrl,
                UserId = request.UserId
            };

            _context.DoubleUrls.Add(doubleUrl);

            await _context.SaveChangesAsync();

            return shortUrl;
        }
    }
}