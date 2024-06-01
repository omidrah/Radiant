using System.Net;
using System.Net.Sockets;
using socketServer.Classes;

namespace socketServer.Models
{
    public class Server
    {
        public Socket _listener;
        public string IpServer { get; set; }
        public int PortServer { get; set; }
        public Server(string Ip, int port)
        {
            IpServer = Ip;
            PortServer = port;
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse(IpServer) ?? IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PortServer);
            _listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                _listener.Bind(localEndPoint);
                _listener.Listen(100);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Dispose()
        {
            if (_listener != null)
            {
                _listener.Shutdown(SocketShutdown.Both);
                _listener.Close();
            }
        }
        public string GetIpAddress()
        {
            IPHostEntry localhost;
            string localAddress = "";
            localhost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress address in localhost.AddressList)
            {
                if (address.AddressFamily.ToString() == "InterNetwork")
                {
                    localAddress = address.ToString();
                }
            }
            return localAddress;
        }
    }
}
