using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Common.Commnads;
using RawRabbit;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    public class ActivitiesController : Controller
    {
        private readonly IBusClient _busClient;

        public ActivitiesController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateActivity commnad)
        {
            commnad.Id = Guid.NewGuid();
            commnad.CreatedTime = DateTime.UtcNow;

            await _busClient.PublishAsync(commnad);
            return Accepted($"activites/{commnad.Id}");
        }
    }

    [Route("[controller]")]
    public class UsersControllerController: Controller
    {
        private readonly IBusClient _busClient;

        public UsersControllerController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpPost("register")] // /users/register
        public async Task<IActionResult> Post([FromBody] CreateUser commnad)
        {
            await _busClient.PublishAsync(commnad);
            return Accepted();
        }
    }
}
