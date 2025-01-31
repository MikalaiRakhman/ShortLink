using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShortLink.BL.Services;
using ShortLink.DAL.Data;
using ShortLink.Domain.Entities;

namespace ShortLink.BL.CreateShortUrl.CreateDoubleUrlWithUserId
{
    public class CreateDoubleUrlWithUserIdCommandHandler : IRequestHandler<CreateDoubleUrlWithUserIdCommand, string>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly UrlService _urlService;
        public CreateDoubleUrlWithUserIdCommandHandler(ApplicationDbContext context, UserManager<User> userManager, UrlService urlService)
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
            var doubleUrl = new DoubleUrl
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