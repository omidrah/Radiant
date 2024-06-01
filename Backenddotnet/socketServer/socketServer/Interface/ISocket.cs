using socketServer.Models;
using System.Net.Sockets;

namespace socketServer.Interface
{
    public interface ISocketListener
    {
        void AcceptCallback(IAsyncResult ar);
        void BeginReceiveCallback(IAsyncResult ar);
        Task CheckValue(StateObject client);
        void ClientDis(StateObject item);
        Task ParseMsg(string content, StateObject client);
        void Send(Socket socket, string data);
        void SendCallback(IAsyncResult ar);
        bool SocketConnected(Socket s, int mode);
        void StartListening();
    }
}