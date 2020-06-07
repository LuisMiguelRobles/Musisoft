namespace API.Controllers
{
    using Application.User;
    using Application.User.Commands;
    using Application.User.Queries;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [AllowAnonymous]
    public class UserController : BaseController
    {
        /// <summary>
        /// Login user
        /// </summary>
              
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

        /// <summary>
        /// Register new user.
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(Register.Command command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Get Current user
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<User>> CurrentUser()
        {
            return await Mediator.Send(new CurrentUser.Query());
        }
    }
}
