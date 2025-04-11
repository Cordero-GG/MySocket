
namespace MySockectLibrary
{
    public interface ISocketServer
    {
        Task StartAsync();
        void Stop();
    }
}
