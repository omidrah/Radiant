using System.Net.Sockets;

namespace socketServer.Models
{
    public static class StaticClass
    {
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public static Socket listener;
    }
}
