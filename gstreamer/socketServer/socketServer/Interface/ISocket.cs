using socketServer.Models;
using System.Net.Sockets;

namespace socketServer.Interface
{
    public interface ISocketListener
    {
        void AcceptCallback(IAsyncResult ar);
        void BeginReceiveCallback(IAsyncResult ar);
        void ClientDis(StateObject item);      
        bool SocketConnected(Socket s, int mode);
        void StartListening();
    }
}