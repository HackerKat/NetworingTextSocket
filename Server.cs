using System;
using System.Net;
using System.Net.Sockets;

public class Class1
{
	public Class1()
	{
        IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
        Socket newSocket = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream, ProtocolType.Tcp);
        newSocket.Bind(localEndPoint);
        newSocket.Listen(10);
        Socket client = newSocket.Accept();
   
	}
}
