using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShortLink.BL.Models;
using ShortLink.BL.User.DeleteUserById;
using ShortLink.BL.User.GetAllUsers;
using ShortLink.BL.User.GetUserById;

namespace ShortLink.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var users = await _mediator.Send(query);

            if (users is null or [])
            {
                return NotFound("User was not found.");
            }

            return Ok(users);
        }

        [HttpGet("get-user/{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id};
            var user = await _mediator.Send(query);

            return Ok(user);
        }

        [HttpDelete("delete-user-by-id/{id}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            var command = new DeleteUserByIdCommand() { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
