using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using WebApplication5.Model;

namespace WebApplication5.Controllers
{
    public class DataHub : Hub
    {
        private readonly ILogger<DataHub> _logger;

        public DataHub(ILogger<DataHub> logger)
        {
            _logger = logger;
        }
        //when called, sends the packet to all connected clients.
        public async Task SendData(RecievePacket packet)
        {
            await Clients.All.SendAsync("ReceiveData", packet);
        }

    }
}
