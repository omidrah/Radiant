using Gst;
using Microsoft.Extensions.Configuration;
using socketServer.Interface;
using socketServer.Models;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace socketServer.Classes
{
    public class SocketListener : ISocketListener
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetDllDirectory(string lpPathName);

        private IActions _actions;
        private readonly IConfiguration _config;
        public SocketListener(IActions actions, IConfiguration config)
        {
            _actions = actions;
           _config =config; 
        }
        public void StartListening()
        {
            var configSection = new Config();
            _config.GetSection("Config").Bind(configSection);
            if (configSection != null)
            {
                int.TryParse(configSection.Port, out int port);
                StaticClass.listener = new Server(configSection.Ip, port)._listener;
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
                        if (SocketConnected(client, 0))
                        {
                            client.BeginReceive(state.buffer, 0, 1024, SocketFlags.None,new AsyncCallback(BeginReceiveCallback), state);
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
                    if (sk_client != null)
                    {
                        if (SocketConnected(sk_client, 0))
                        {
                            int bytesRead = sk_client.EndReceive(ar);                           
                            if (bytesRead > 0)
                            {
                                //Console.WriteLine(client.value);
                                //Array.Clear(client.buffer, 0, client.buffer.Length);


                                bool setBinDir = SetDllDirectory(@"D:\GStreamer\1.0\mingw_x86_64\bin");
                                Gst.Application.Init();
                                string pipelineDescription = "gst-launch-1.0 -e rtspsrc location=\"rtsp://10.42.0.3:554\"  user-id=admin user-pw=abc123321bca latency=100 ! rtph264depay ! capsfilter caps=\"video/x-h264,width=640,height=480,framerate=(fraction)25/1\" ! mp4mux ! filesink location=video.mp4";

                                var pipeline = Parse.Launch(pipelineDescription) as Pipeline;
                                if (pipeline == null)
                                {
                                    Console.WriteLine("Failed to create pipeline.");
                                    return;
                                }
                                // Start playing the pipeline
                                pipeline.SetState(State.Playing);

                                // Wait until error or EOS
                                //var bus = pipeline.Bus;
                                //var msg = bus.TimedPopFiltered(Constants.CLOCK_TIME_NONE, MessageType.Eos | MessageType.Error);
                                //// Free resources
                                //pipeline.SetState(State.Null);
                                //pipeline.Dispose();


                                try
                                {
                                    if (client.IsConnected)
                                    {
                                        if (sk_client.Connected)
                                        {
                                            sk_client.BeginReceive(client.buffer, 0, 1024, SocketFlags.None, new AsyncCallback(BeginReceiveCallback), client);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    await _actions.LogErrorAsync(ex, $"BeginReceiveCallback>>>Exception", $"state =>  {client.IsConnected}").ConfigureAwait(false);
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

            if (part1 && part2) //|| !s.Connected)
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
