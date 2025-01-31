using Microsoft.AspNetCore.Mvc;
using MediatR;
using ShortLink.BL.DoubleUrl.GetOriginalUrl;
using ShortLink.BL.DoubleUrl.CreateShortUrl.CreateDoubleUrl;
using ShortLink.BL.DoubleUrl.CreateShortUrl.CreateDoubleUrlWithUserId;
using ShortLink.BL.DoubleUrl.DeleteDoubleUrl;
using ShortLink.BL.Models;
using ShortLink.BL.User.GetAllUsers;
using ShortLink.BL.DoubleUrl.GetAllDoubleUrls;

namespace ShortLink.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortUrlsController : Controller
    {
        private readonly IMediator _mediator;

        public ShortUrlsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all-double-urls")]
        public async Task<ActionResult<List<Domain.Entities.DoubleUrl>>> GetAllDoubleUrls()
        {
            var query = new GetAllDoubleUrlsQuery();
            var doubleUrls = await _mediator.Send(query);

            if (doubleUrls is null or [])
            {
                return NotFound("Urls was not found.");
            }

            return Ok(doubleUrls);
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

        [HttpDelete("delete-double-url/{id:guid}")]
        public async Task<IActionResult> DeleteDuobleUrl(Guid id)
        {
            var command = new DeleteDoubleUrlCommand { Id = id };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}