📄 README.md:

```markdown
# MySockectLibrary

Este proyecto implementa una librería en C# para comunicación cliente-servidor utilizando sockets TCP. La lógica de conexión, envío y recepción de datos está desacoplada mediante interfaces, permitiendo su reutilización desde aplicaciones externas mediante una DLL.

##  Estructura del repositorio

```
/MySocket
├── /MySockectLibrary         → Biblioteca principal (genera el .dll)
│   ├── ISocketClient.cs
│   ├── ISocketServer.cs
│   ├── SocketClient.cs
│   ├── SocketServer.cs
│   └── MySockectLibrary.csproj
├── /ClienteFinal             → Aplicación cliente que consume la DLL
│   ├── Program.cs
│   └── ClienteFinal.csproj
├── /ServidorFinal (opcional) → Aplicación servidor que consume la DLL
│   ├── Program.cs
│   └── ServidorFinal.csproj
├── /build                    → DLL generada para pruebas directas
│   └── MySockectLibrary.dll
├── MySocket.sln              → Solución de Visual Studio o VS Code
└── README.md                 → Este archivo
```

##  ¿Qué hace esta librería?

- Implementa interfaces ISocketClient e ISocketServer para desacoplar la lógica
- Usa programación asíncrona (async/await) para evitar bloqueos
- Se puede usar desde cualquier aplicación que consuma la DLL

##  Cómo compilar

Desde la raíz del proyecto:

```bash
dotnet build
```

Esto compilará todos los proyectos y generará la DLL en:

```
MySockectLibrary/bin/Debug/net8.0/MySockectLibrary.dll
```

También podés usar la copia precompilada en la carpeta /build.

##  Cómo ejecutar

En terminal, desde la carpeta raíz:

Ejecutar el servidor (en una terminal):

```bash
dotnet run --project Server
```

Ejecutar el cliente (en otra terminal):

```bash
dotnet run --project Cliente
```

El cliente se conectará al servidor y enviará un mensaje a través de la DLL.

##  Autores
Adam Cheng Liang
David Cordero Zúñiga
Raul Ramirez Villegas

Este proyecto fue desarrollado como parte de una tarea académica para demostrar el uso de sockets, programación asíncrona y desacoplamiento con interfaces en .NET 8.

```

