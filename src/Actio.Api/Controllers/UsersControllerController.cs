using System.Threading.Tasks;
using Actio.Common.Commnads;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.Api.Controllers
{
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