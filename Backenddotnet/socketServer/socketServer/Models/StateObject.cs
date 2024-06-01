using System.Net.Sockets;
using socketServer.Classes;

namespace socketServer.Models
{
    public class StateObject
    {
        // Client  socket.  
        public virtual Socket workSocket { get; set; }
        // Receive buffer.  
        public virtual byte[] buffer { get; set; }
        // Received data string.  
        public virtual string value { get; set; }
        public string attr { get; set; }
        public string couple { get; set; }
        public bool IsConnected { get; set; }
    }
}
