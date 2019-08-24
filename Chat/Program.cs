using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.ComponentModel;

namespace Server
{
    class Program
    {
        static private BackgroundWorker bwListener;

        static void Main(string[] args)
        {
            bwListener = new BackgroundWorker();
            bwListener.DoWork += new DoWorkEventHandler(StartToListen);
            bwListener.RunWorkerAsync();


            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
            Socket newSocket = new Socket(AddressFamily.InterNetwork,
                                            SocketType.Stream, ProtocolType.Tcp);


            newSocket.Bind(localEndPoint);
            //backlog: connections queued to be acepted
            newSocket.Listen(10);
            Socket client = newSocket.Accept();
        }

        peivate void StartToListe
    }
}
