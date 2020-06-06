namespace API.Controllers
{
    using Application.Company.Commands;
    using Application.Company.Queries;
    using Domain;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CompanyController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Company>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await Mediator.Send(command);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Create.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id, Create.Command command)
        {
            command.Id = id;
            return await Mediator.Send(new Delete.Command { Id = id });
        }
    }
}
