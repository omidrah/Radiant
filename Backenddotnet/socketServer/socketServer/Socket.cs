using Microsoft.Extensions.Configuration;
using socketServer.Interface;
using socketServer.Models;
using System.Net.Sockets;
using System.Text;

namespace socketServer
{
    public class AsynchronousSocketListener
    {
        private IActions _actions;
        private IConfiguration _config;
        public AsynchronousSocketListener(IActions actions, IConfiguration config) {
            _actions = actions;
            _config = config;
        }
        public void StartListening()
        {
            StaticClass.listener = new Server(ip, port)._listener;
            while (true)
            {
                // Set the event to nonsignaled state.  
                StaticClass.allDone.Reset();
                // Start an asynchronous socket to listen for connections.   
                StaticClass.listener.BeginAccept(new AsyncCallback(AcceptCallback), StaticClass.listener);
                // Wait until a connection is made before continuing.  
                StaticClass.allDone.WaitOne();
            }
        }

        public async void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                // Signal the main thread to continue.  
                StaticClass.allDone.Set();
                // Get the socket that handles the client request.  
                if (ar.IsCompleted)
                {
                    Socket client = ((Socket)ar.AsyncState).EndAccept(ar);
                    // Create the state object.                      
                    StateObject state = new()
                    {
                        workSocket = client,
                        IsConnected = false,
                        value = string.Empty,
                        buffer = new byte[1024]
                    };
                    try
                    {
                        //Console.WriteLine($"\n Accept Socket Ip = {state.IP} @ {DateTime.Now.ToString("yyyy/M/d HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)} \n");
                        if (SocketConnected(client, 0))
                        {
                            client.BeginReceive(state.buffer, 0, 1024, SocketFlags.None,
                                new AsyncCallback(BeginReceiveCallback), state);
                        }
                    }
                    catch (Exception e)
                    {
                        await _actions.LogErrorAsync(e, "from AcceptCallback has Excetion ").ConfigureAwait(false);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                await _actions.LogErrorAsync(e, "84  AcceptCallback").ConfigureAwait(false);
            }
        }
        public async void BeginReceiveCallback(IAsyncResult ar)
        {
            if (ar.IsCompleted)
            {
                var client = (StateObject)ar.AsyncState;
                if (client != null)
                {
                    var sk_client = client.workSocket;
                    // Read data from the client socket.   
                    if (sk_client != null)
                    {
                        if (SocketConnected(sk_client, 0))
                        {
                            int bytesRead = sk_client.EndReceive(ar);
                            if (bytesRead > 0)
                            {
                                client.value = Encoding.ASCII.GetString(client.buffer, 0, bytesRead).ToString();
                                //Console.WriteLine(client.value);
                                await CheckValue(client).ConfigureAwait(false);
                                Array.Clear(client.buffer, 0, client.buffer.Length);
                                try
                                {
                                    if (client.IsConnected)
                                    {
                                        if (sk_client.Connected)
                                        {
                                            sk_client.BeginReceive(client.buffer, 0, 1024, SocketFlags.None, new AsyncCallback(BeginReceiveCallback), client);
                                        }
                                        else
                                        {
                                            
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    await _actions.LogErrorAsync(ex, $"BeginReceiveCallback>>>Exception",
                                              $"state =>  {client.IsConnected}").ConfigureAwait(false);
                                }
                            }
                        }
                    }
                }
            }
        }
       
        public void ClientDis(StateObject item)
        {
            try
            {
                if (item.workSocket != null)
                {
                    try
                    {
                        /*accoridg to 
                         https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.close?redirectedfrom=MSDN&view=net-7.0#System_Net_Sockets_Socket_Close
                        */
                        //item.workSocket.LingerState.Enabled = false;
                        //item.workSocket.LingerState.LingerTime = 300;
                        item.workSocket.Shutdown(SocketShutdown.Both);
                    }
                    catch (SocketException sex)
                    {
                        _actions.LogErrorAsync(sex, "from clientDis 3 --Socket Except ");

                    }
                    catch (Exception ex)
                    {
                        _actions.LogErrorAsync(ex, "from clientDis  4 --Socket Except  ");
                    }
                    finally
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Socket ShutDown has error" + ex.Message);
                _actions.LogErrorAsync(ex, item.workSocket.ToString()).ConfigureAwait(false);
            }
        }
        public async Task CheckValue(StateObject client)
        {
            await ParseMsg(client.value, client).ConfigureAwait(false);

        }
        public async Task ParseMsg(string content, StateObject client)
        {
            try
            {
                Console.WriteLine(content);
            }
            catch (Exception ex)
            {
                await _actions.LogErrorAsync(ex, "split content").ConfigureAwait(false);
            }

        }
        public async void Send(Socket socket, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            // Begin sending the data to the remote device.             
            if (SocketConnected(socket, 0) && byteData.Length > 0)
            {
                try
                {
                    socket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
                }
                catch (Exception od)
                {

                    await _actions.LogErrorAsync(od, $"Send>> BeginSend  @ {DateTime.Now} ").ConfigureAwait(false);
                }
            }

        }
        public void SendCallback(IAsyncResult ar)
        {
            if (ar.IsCompleted)
            {
                Socket socket = (Socket)ar.AsyncState;
                if (SocketConnected(socket, 0))
                {
                    int bytesSent = socket.EndSend(ar);
                }
            }

        }
        /// <summary>
        /// Socket Connected
        /// </summary>
        /// <param name="s">socket</param>
        /// <param name="mode">0=read , 1= write, 2=error</param>
        /// <returns></returns>
        public bool SocketConnected(Socket s, int mode)
        {

            if (s == null) return false;

            bool part1 = s.Poll(1, mode == 0 ? SelectMode.SelectRead : mode == 1 ? SelectMode.SelectWrite : SelectMode.SelectError);

            bool part2 = s.Available == 0;

            if ((part1 && part2)) //|| !s.Connected)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }


}
