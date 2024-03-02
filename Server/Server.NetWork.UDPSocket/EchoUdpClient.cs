using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server.NetWork.UDPSocket;

public class EchoUdpClient : UdpClient
{
    public EchoUdpClient(string address, int port) : base(address, port)
    {
    }

    public void DisconnectAndStop()
    {
        stop = true;
        Disconnect();
        while (IsConnected)
            Thread.Yield();
    }

    protected override void OnConnected()
    {
        Console.WriteLine($"Echo UDP client connected a new session with Id {Id}");

        // Start receive datagrams
        ReceiveAsync();
    }

    protected override void OnDisconnected()
    {
        Console.WriteLine($"Echo UDP client disconnected a session with Id {Id}");

        // Wait for a while...
        Thread.Sleep(1000);

        // Try to connect again
        if (!stop)
            Connect();
    }

    protected override void OnReceived(EndPoint endpoint, byte[] buffer, long offset, long size)
    {
        Console.WriteLine("Incoming: " + Encoding.UTF8.GetString(buffer, (int)offset, (int)size));

        // Continue receive datagrams
        ReceiveAsync();
    }

    protected override void OnError(SocketError error)
    {
        Console.WriteLine($"Echo UDP client caught an error with code {error}");
    }

    private bool stop;
}