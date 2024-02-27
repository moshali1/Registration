using Microsoft.AspNetCore.Authorization;

namespace RegistrationPortal.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) =>
            _userService = userService;


        [HttpGet("from-auth/{objectId}")]
        public async Task<ActionResult<UserDto>> GetUserFromAuthentication(string objectId)
        {
            return await _userService.GetUserFromAuthentication(objectId);
        }
    }
}
