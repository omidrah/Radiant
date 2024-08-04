using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApplication5.Model;

namespace WebApplication5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly IHubContext<DataHub> _hubContext;

        public TestController(IHubContext<DataHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet("sendTest")]
        public async Task<IActionResult> SendTest()
        {
            var testData = new 
            {
                head = "TEST",
                upPower = new Random().Next(100,425000),
                m1_addr = 2,
                m1_xt = 3,
                // Initialize other properties...
            };

            await _hubContext.Clients.All.SendAsync("ReceiveData", testData);
            return Ok();
        }
    }
}
