using Microsoft.AspNetCore.Mvc;
using MediatR;
using ShortLink.BL.GetOriginalUrl;
using ShortLink.BL.CreateShortUrl.CreateDoubleUrlWithUserId;
using ShortLink.BL.CreateShortUrl.CreateDoubleUrl;

namespace ShortLink.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlController : Controller
    {
        private readonly IMediator _mediator;

        public ShortUrlController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("get-original-url")]
        public async Task<IActionResult> GetOriginalUrl([FromBody] GetOriginalUrlCommand command)
        {
            var originalUrl = await _mediator.Send(command);

            return Ok(new { originalUrl });
        }

        [HttpPost("create-short-url-with-user-id")]
        public async Task<IActionResult> CreateShortUrlWithUserId([FromBody] CreateDoubleUrlWithUserIdCommand command)
        {
            var shortUrl = await _mediator.Send(command);
            return Ok(new { shortUrl });
        }

        [HttpPost("create-short-url")]
        public async Task<IActionResult> CreateShortUrl([FromBody] CreateDoubleUrlCommand command)
        {
            var shortUrl = await _mediator.Send(command);
            return Ok(new { shortUrl });
        }
    }
}
