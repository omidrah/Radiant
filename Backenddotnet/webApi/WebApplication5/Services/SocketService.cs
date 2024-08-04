using Microsoft.AspNetCore.SignalR;
using System.Net.Sockets;
using System.Net;
using WebApplication5.Controllers;
using WebApplication5.Model;
using Microsoft.Extensions.Options;

namespace WebApplication5.Services
{
    public class SocketService
    {
        private readonly ILogger<SocketService> _logger;
        private readonly FileService _fileService;
        private readonly SocketConfig _socketConfig;
        public SocketService(FileService fileService, ILogger<SocketService> logger, IOptions<Settings> settings)
        {           
            _fileService = fileService;
            _logger = logger;
            _socketConfig = settings.Value.SocketConfig;
        }
        public async Task<RecievePacket> SendDataAsync(byte[] inputArray)
        {
            
            using Socket client = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipaddr = IPAddress.Parse(_socketConfig.IpAddress);
            RecievePacket packet = null;
            try
            {
                _logger.LogInformation($"{DateTime.Now}");
                Console.WriteLine($"IPAddress: {ipaddr} - Port: {_socketConfig.Port}");
                await client.ConnectAsync(ipaddr, _socketConfig.Port);
                Console.WriteLine("Connected to server.");
                await client.SendAsync(new ArraySegment<byte>(inputArray), SocketFlags.None);
                Console.WriteLine("Data sent to server.");

                byte[] buffer = new byte[324];
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
                /*if echo in response*/
                //byte[] recbuf = new byte[324];
                //Array.Copy(buffer, 110, recbuf, 0, 324);
                // Convert byte array to recieve object
                //RecievePacket packet = DataConverter.ByteArrayToDataPacket(recBuffer);
                //DataConverter.showByteArray(buffer, asciiBuilder, bytesReceived);

                packet = DataConverter.ParseDataPacket(buffer);
                // Receive Data Save to file
                await ReceiveDataSaveAsync(packet);              
                // Log the packet data on console
                ConsoLogPacket(packet);
               
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
            return packet;
        }
        private async Task ReceiveDataSaveAsync(RecievePacket packet)
        {
            byte[] byteArray = DataConverter.ToByteArray(packet);
            await _fileService.ReceiveDataToFileAsync(byteArray);
        }
        private void ConsoLogPacket(RecievePacket packet)
        {
            Console.WriteLine($"Header: {new string(packet.Head)}");
            Console.WriteLine($"Up Power: {packet.UpPower}");

            Console.WriteLine($"Xt: {packet.M1_Xt}");
            Console.WriteLine($"Yt: {packet.M1_Yt}");
            Console.WriteLine($"Zt: {packet.M1_Zt}");
            Console.WriteLine($"Xm: {packet.M1_Xm}");
            Console.WriteLine($"Ym: {packet.M1_Ym}");
            Console.WriteLine($"Zm: {packet.M1_Zm}");
            Console.WriteLine($"Vxm: {packet.M1_Vxm}");
            Console.WriteLine($"Vym: {packet.M1_Vym}");
            Console.WriteLine($"Vzm: {packet.M1_Vzm}");
            Console.WriteLine($"Vxt: {packet.M1_Vxt}");
            Console.WriteLine($"Vyt: {packet.M1_Vyt}");
            Console.WriteLine($"Vzt: {packet.M1_Vzt}");
            
            Console.WriteLine($"Checksum: {packet.CheckSum}");
            Console.WriteLine($"Footer: {new string(packet.Footer)}");
        }
    }
}
