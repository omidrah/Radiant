using Microsoft.AspNetCore.SignalR;
using System.Net.Sockets;
using System.Net;
using WebApplication5.Controllers;
using WebApplication5.Model;

namespace WebApplication5.Services
{
    public class SocketService
    {
        private readonly IHubContext<DataHub> _hubContext;
        private readonly ILogger<SocketService> _logger;

        public SocketService(IHubContext<DataHub> hubContext, ILogger<SocketService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task SendDataAsync(byte[] inputArray, string serverIp= "192.168.1.15", int serverPort=7)
        {
            using Socket client = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddr = IPAddress.Parse(serverIp);

            try
            {
                _logger.LogInformation($"{DateTime.Now}");
                Console.WriteLine($"IPAddress: {ipaddr} - Port: {serverPort}");
                await client.ConnectAsync(ipaddr, serverPort);
                Console.WriteLine("Connected to server.");
                await client.SendAsync(new ArraySegment<byte>(inputArray), SocketFlags.None);
                Console.WriteLine("Data sent to server.");

                byte[] buffer = new byte[152];
                int bytesReceived = 0;

                while (true)
                {
                    int nRecv = await client.ReceiveAsync(new ArraySegment<byte>(buffer, bytesReceived, buffer.Length - bytesReceived), SocketFlags.None);
                    if (nRecv == 0)
                    {
                        break; // The connection has been closed by the server.
                    }
                    bytesReceived += nRecv;
                    // Assume the end of the message is when the buffer is full or another condition
                    if (bytesReceived >= buffer.Length)
                    {
                        break;
                    }
                }

                int startind = 108; // Start index of the packet
                int endind = 152; // End index of the packet
                int packetLen = endind - startind;
                byte[] recBuffer = new byte[packetLen];
                Array.Copy(buffer, startind, recBuffer, 0, packetLen);

                // Convert byte array to recieve object
                //RecievePacket packet = DataConverter.ByteArrayToDataPacket(recBuffer);
                //DataConverter.showByteArray(buffer, asciiBuilder, bytesReceived);

                RecievePacket packet = DataConverter.ParseDataPacket(recBuffer);

                // Save to file
                await SaveDataToFileAsync(packet);

                // Send to clients via SignalR
                await _hubContext.Clients.All.SendAsync("ReceiveData", packet);

                // Log the packet data
                LogPacket(packet);
            }
            catch (Exception excp)
            {
                Console.WriteLine(excp.ToString());
            }
            finally
            {
                if (client.Connected)
                {
                    client.Shutdown(SocketShutdown.Both);
                }
                client.Close();
                client.Dispose();
            }
        }

        private async Task SaveDataToFileAsync(RecievePacket packet)
        {
            // Implement your logic to save packet data to a file
        }

        private void LogPacket(RecievePacket packet)
        {
            Console.WriteLine($"Header: {new string(packet.Head)}");
            Console.WriteLine($"Missile Address: {packet.MissleAddress}");
            Console.WriteLine($"Up Power: {packet.UpPower}");
            Console.WriteLine($"Xt: {packet.Xt}");
            Console.WriteLine($"Yt: {packet.Yt}");
            Console.WriteLine($"Zt: {packet.Zt}");
            Console.WriteLine($"Xm: {packet.Xm}");
            Console.WriteLine($"Ym: {packet.Ym}");
            Console.WriteLine($"Zm: {packet.Zm}");
            Console.WriteLine($"Vxm: {packet.Vxm}");
            Console.WriteLine($"Vym: {packet.Vym}");
            Console.WriteLine($"Vzm: {packet.Vzm}");
            Console.WriteLine($"Vxt: {packet.Vxt}");
            Console.WriteLine($"Vyt: {packet.Vyt}");
            Console.WriteLine($"Vzt: {packet.Vzt}");
            Console.WriteLine($"Ctrl: {packet.Ctrl}");
            Console.WriteLine($"Reset Time: {packet.ResetTime}");
            Console.WriteLine($"CRC16: {packet.CRC16}");
            Console.WriteLine($"Checksum: {packet.CheckSum}");
            Console.WriteLine($"Footer: {new string(packet.Footer)}");
        }
    }
}
