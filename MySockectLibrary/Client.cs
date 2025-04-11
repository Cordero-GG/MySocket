using System.Net;
using System.Net.Sockets;
using System.Text;
using MySockectLibrary; // correcto

/// <summary>
/// Clase que representa un cliente de socket TCP.
/// </summary>
namespace MySockectLibrary
{
public class SocketClient:ISocketClient
{
    private TcpClient _client = null!;
    private NetworkStream _stream = null!;
    private readonly int _maxRetries;
    private readonly int _retryDelayMs;

    public SocketClient(int maxRetries = 3, int retryDelayMs = 1000)
    {
        try
        {
        _maxRetries = maxRetries;
        _retryDelayMs = retryDelayMs;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            throw new ArgumentOutOfRangeException("Los parámetros de reintento no son válidos", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al inicializar el cliente de socket", ex);
        }
    }

    public async Task ConnectAsync(string ip, int port)
    {
        int retryCount = 0;
        while (retryCount < _maxRetries)
        {
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(ip, port);
                _stream = _client.GetStream();
                Console.WriteLine($"Conectado a {ip}:{port}");
                return;
            }
            catch (Exception ex) when (ex is SocketException || ex is IOException)
            {
                retryCount++;
                Console.WriteLine($"Intento {retryCount} de conexión fallido: {ex.Message}");

                if (retryCount >= _maxRetries)
                    throw new Exception($"No se pudo conectar después de {_maxRetries} intentos", ex);

                await Task.Delay(_retryDelayMs);
            }
        }
    }

    public async Task SendAsync(string message)
    {
        try
        {
        if (_client == null || !_client.Connected)
        {
            throw new InvalidOperationException("El cliente no está conectado");
        }

        int retryCount = 0;
        byte[] data = Encoding.UTF8.GetBytes(message);

        while (retryCount < _maxRetries)
        {
            try
            {
                await _stream!.WriteAsync(data, 0, data.Length);

                Console.WriteLine($"Mensaje enviado: {message}");

                byte[] buffer = new byte[1024];
                int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);

                if (bytesRead == 0)
                    throw new SocketException((int)SocketError.ConnectionReset);

                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Respuesta del servidor: {response}");
                return;
            }
            catch (Exception ex) when (ex is SocketException || ex is IOException)
            {
                retryCount++;
                Console.WriteLine($"Intento {retryCount} de envío fallido: {ex.Message}");

                if (retryCount >= _maxRetries)
                    throw;

                await Task.Delay(_retryDelayMs);
                await TryReconnectAsync();
            }
       
        }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar el mensaje: {ex.Message}");
            throw;
        }
    }

    private async Task TryReconnectAsync()
    {
        try
    {
        if (_client?.Connected == false && _client?.Client?.RemoteEndPoint is IPEndPoint endPoint)
        {
            Console.WriteLine("Intentando reconectar...");
            await ConnectAsync(endPoint.Address.ToString(), endPoint.Port);
        }
    }
    catch
    {
        // Silenciar errores de reconexión, ya que el reintento principal manejará esto
    }
    }

    public void Disconnect()
    {
        try
        {
        _stream?.Close();
        _client?.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al desconectar: {ex.Message}");
            throw;
        }
        finally
        {
            _client = null!;// Liberar el cliente
            _stream = null!;// Liberar el stream
        }
    }
}

}