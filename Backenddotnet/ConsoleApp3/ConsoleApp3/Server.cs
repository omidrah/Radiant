//using System.Net;
//using System.Net.Sockets;
//using System.Text;

//namespace ConsoleApp3
//{
//    public class Server
//    {
//        static async Task Main(string[] args)
//        {
//            var listener = new TcpListener(IPAddress.Loopback, 65432);
//            listener.Start();
//            Console.WriteLine("Server started. Waiting for a connection...");
//            while (true)
//            {
//                var client = await listener.AcceptTcpClientAsync();
//                _ = Task.Run(() => HandleClient(client));
//            }
//        }
//        private static async Task HandleClient(TcpClient client)
//        {
//            var buffer = new byte[1024];
//            var stream = client.GetStream();
//            var clientEndPoint = client.Client.RemoteEndPoint.ToString();
//            Console.WriteLine($"Connected by {clientEndPoint}");

//            while (true)
//            {
//                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
//                if (bytesRead == 0) break;

//                var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//                Console.WriteLine($"Received: {message}");

//                await stream.WriteAsync(buffer, 0, bytesRead);
//            }

//            client.Close();
//            Console.WriteLine($"Connection with {clientEndPoint} closed.");
//        }
//    }
//}
