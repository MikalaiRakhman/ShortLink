using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShortLink.DAL.Data;

namespace ShortLink.Web.Controllers
{
    [Route("api/{shortUrl}")]
    [ApiController]
    public class RedirectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RedirectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> RedirectToOriginalUrl(string shortUrl)
        {
            var doubleUrl = await _context.DoubleUrls.FirstOrDefaultAsync(d => d.ShortUrl == shortUrl);
            if (doubleUrl == null)
            {
                return NotFound("Original URL not found.");
            }

            return Redirect(doubleUrl.OriginalUrl);
        }
    }
}
