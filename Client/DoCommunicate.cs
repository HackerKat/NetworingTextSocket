using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading;
using Chat = System.Net;
using System.Collections;
using System.Net.Sockets;

namespace ChatProgram
{
    public class DoCommunicate
    {
        public TcpClient client;
        public StreamReader reader;
        public StreamWriter writer;
        public string nickName;

        public DoCommunicate(System.Net.Sockets.TcpClient tcpClient)
        {
            client = tcpClient;
            Thread chatThread = new Thread(new ThreadStart(startChat));
            chatThread.Start();
        }

        private string GetNick()
        {
            writer.WriteLine("What is your nickname? ");
            writer.Flush();
            return reader.ReadLine();
        }

        private void startChat()
        {
            reader = new System.IO.StreamReader(client.GetStream());
            writer = new System.IO.StreamWriter(client.GetStream());
            writer.WriteLine("Welcome to Chat");
            nickName = GetNick();

            while (ChatServer.nickName.Contains(nickName))
            {
                writer.WriteLine("ERROR - Nickname already exists! Please try a new one");
                nickName = GetNick();
            }
            ChatServer.nickName.Add(nickName, client);
            ChatServer.nickNameByConnect.Add(client, nickName);
            ChatServer.SendSysMsg("**" + nickName + "** has joined the room");
            writer.WriteLine("Now talking .... \r\n-----------------");
            writer.Flush();
            Thread chatThread = new Thread(new ThreadStart(runChat));
            chatThread.Start();
        }

        private void runChat()
        {
            try
            {
                string line = "";
                while (true)
                {
                    line = reader.ReadLine();
                    ChatServer.SendMsgToAll(nickName, line);
                }
            }
            catch (Exception e44)
            {
                Console.WriteLine(e44);
            }
        }


        
    }
}
