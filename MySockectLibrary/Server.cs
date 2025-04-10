﻿using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using MySockectLibrary; // correcto


namespace MySockectLibrary
{

    public class SocketServer: ISocketServer
    {
        private TcpListener _listener;
        private bool _isRunning;
        private readonly int _maxRetries;
        private readonly int _retryDelayMs;

        public SocketServer(int port, int maxRetries = 3, int retryDelayMs = 1000)
        {
            try
            {
<<<<<<< HEAD
                // Inicializa el listener TCP en la dirección IP local y el puerto especificado.
                _listener = new TcpListener(IPAddress.Any, port);
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error al inicializar el servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
=======
            _listener = new TcpListener(IPAddress.Any, port);
            _maxRetries = maxRetries;
            _retryDelayMs = retryDelayMs;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("Los parámetros de reintento no son válidos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar el servidor de socket", ex);
>>>>>>> ST-Branch
            }
        }

        public async Task StartAsync()
        {
<<<<<<< HEAD
            try{
=======
            try
            {
>>>>>>> ST-Branch
            _isRunning = true;
            _listener.Start();
            Console.WriteLine($"Server iniciado en puerto {((IPEndPoint)_listener.LocalEndpoint).Port}");

            while (_isRunning)
            {
                try
                {
<<<<<<< HEAD
                var client = await _listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client); // Manejar cliente en segundo plano
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Error al aceptar conexión: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inesperado: {ex.Message}");
                }
            }
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error al iniciar el servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
=======
                    var client = await _listener.AcceptTcpClientAsync();
                    _ = HandleClientWithRetryAsync(client);
                }
                catch (Exception ex) when (ex is SocketException || ex is ObjectDisposedException)
                {
                    if (!_isRunning) return; // Detención normal del servidor

                    Console.WriteLine($"Error aceptando conexión: {ex.Message}");
                    await Task.Delay(_retryDelayMs);
                }
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar el servidor: {ex.Message}");// Manejo de excepciones al iniciar el servidor
            }
            finally
            {
                _listener.Stop(); // Asegúrate de detener el listener al finalizar
            }
        }

        private async Task HandleClientWithRetryAsync(TcpClient client)
        {
            int retryCount = 0;

            while (retryCount < _maxRetries)
            {
                try
                {
                    await HandleClientAsync(client);
                    return;
                }
                catch (Exception ex) when (ex is SocketException || ex is IOException)
                {
                    retryCount++;
                    Console.WriteLine($"Intento {retryCount} de manejo de cliente fallido: {ex.Message}");

                    if (retryCount >= _maxRetries)
                    {
                        Console.WriteLine($"No se pudo manejar el cliente después de {_maxRetries} intentos");
                        return;
                    }

                    await Task.Delay(_retryDelayMs);
                }
>>>>>>> ST-Branch
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
<<<<<<< HEAD
            {
            using (client)
            using (var stream = client.GetStream())
            {
                try
                {
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Mensaje recibido: {message}");

                // Respuesta automática
                string response = "Mensaje recibido por el servidor";
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error de IO: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inesperado: {ex.Message}");
                }
            }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error de IO: {ex.Message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Error de socket: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
=======
            {
                using (var stream = client.GetStream())
                {
                    while (true) // Mantener la conexión abierta para múltiples mensajes
                    {
                        try
                        {
                        byte[] buffer = new byte[1024];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                        if (bytesRead == 0) // Cliente cerró la conexión
                            break;

                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Mensaje recibido: {message}");

                        string response = $"{message}";
                        byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                        await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine($"Error de E/S: {ex.Message}");
                            break; // Salir del bucle si hay un error de E/S
                        }
                    }
                }
            }
            finally
            {
                client.Dispose();
>>>>>>> ST-Branch
            }
        }

        public void Stop()
        {
            try
            {
            _isRunning = false;
            _listener.Stop();
            }
<<<<<<< HEAD
            catch (SocketException ex)
            {
                Console.WriteLine($"Error al detener el servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
=======
            catch (Exception ex)
            {
                Console.WriteLine($"Error al detener el servidor: {ex.Message}");// Manejo de excepciones al detener el servidor    
>>>>>>> ST-Branch
            }
        }
    }
}