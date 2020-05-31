namespace API.Controllers
{
    using Application.Email.Commands;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class EmailSenderController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Send(EmailSender.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
