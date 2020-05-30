
namespace API.Controllers
{
    using Application.Campaign.Queries;
    using Domain;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CampaignController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Campaign>>> List()
        {
            return await Mediator.Send(new List.Query());
        }
    }
}
