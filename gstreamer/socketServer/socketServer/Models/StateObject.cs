using System.Net.Sockets;

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
        public bool IsConnected { get; set; }
    }
}
