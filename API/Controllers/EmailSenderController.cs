using System;

namespace API.Controllers
{
    using Application.Email.Commands;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class EmailSenderController : BaseController
    {


        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Guid id, EmailSender.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}
