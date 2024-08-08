//using System.Net.Sockets;
//using System.Text;

//namespace ConsoleApp3
//{
//    public class Client
//    {
//        static async Task Main(string[] args)
//        {
//            using var client = new TcpClient();
//            await client.ConnectAsync("127.0.0.1", 65432);
//            Console.WriteLine("Connected to the server.");
//            var stream = client.GetStream();

//            while (true)
//            {
//                Console.Write("Enter message to send: ");
//                var message = Console.ReadLine();
//                var buffer = Encoding.UTF8.GetBytes(message);

//                await stream.WriteAsync(buffer, 0, buffer.Length);

//                var responseBuffer = new byte[1024];
//                var bytesRead = await stream.ReadAsync(responseBuffer, 0, responseBuffer.Length);
//                var responseMessage = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);

//                Console.WriteLine($"Received: {responseMessage}");
//            }
//        }
//    }
//}
