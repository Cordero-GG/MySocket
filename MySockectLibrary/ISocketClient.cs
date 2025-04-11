public interface ISocketClient
{
    Task ConnectAsync(string ip, int port);
    Task SendAsync(string message);
    void Disconnect();
}
