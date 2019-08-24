using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatAppReloaded
{
    class Program
    {
        private static TcpClient client;


        static void Main(string[] args)
        {
            string server = Console.ReadLine();

            client = new TcpClient();
            client.Connect(server, 4296);

            Thread chatThread = new Thread(new ThreadStart(Reader));
            chatThread.Start();
            StreamWriter writer = new StreamWriter(client.GetStream());
            while (true)
            {
                writer.WriteLine(Console.ReadLine());
                writer.Flush();
            }
        }

        static void Reader()
        {
            StreamReader reader = new StreamReader(client.GetStream());
            while (true)
            {
                Console.WriteLine(reader.ReadLine());
            }
        }

    }
}
