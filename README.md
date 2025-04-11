# MySocket
 Biblioteca de uso de sockets
##  Uso de la biblioteca `MySocket.dll`

Esta biblioteca proporciona una interfaz sencilla, reutilizable y desacoplada para trabajar con sockets en C#. A continuación, se muestran ejemplos de cómo utilizar las clases `SocketServer` y `SocketClient` mediante sus respectivas interfaces.

---

###  Servidor TCP

```csharp
using MySockectLibrary;

ISocketServer server = new SocketServer(port: 5000);
await server.StartAsync();
