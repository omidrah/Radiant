 using Microsoft.Extensions.Options;
using System.Data;
using System.Net;
using System.Net.Sockets;
using WebApplication5.Model;

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
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine("*******************************************************************");
                Console.WriteLine($"*       Socket on {ipaddr} :{_socketConfig.Port}                                 *");
                await client.ConnectAsync(ipaddr, _socketConfig.Port);
                Console.WriteLine($"*       Connected to server@{DateTime.Now}                   *");
                await client.SendAsync(new ArraySegment<byte>(inputArray), SocketFlags.None);
                Console.WriteLine($"*       Sending @ {DateTime.Now}                             *");

                byte[] buffer = new byte[324];//receive packet 
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
                //RecievePacket packet = Utils.ByteArrayToDataPacket(recBuffer);
                //Utils.showByteArray(buffer, asciiBuilder, bytesReceived);

                await _fileService.SaveReceivePacketAsync(buffer); //save buffer in file

                packet = Utils.BufferToRecievePacket(buffer); //parse buffer to recievepacket               
                
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
       
    }
}
