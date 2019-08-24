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

namespace ChatProgram
{
    public class ChatServer
    {
        System.Net.Sockets.TcpListener chatServer;
        public static Hashtable nickName;
        public static Hashtable nickNameByConnect;

        public ChatServer()
        {
            nickName = new Hashtable(100);
            nickNameByConnect = new Hashtable(100);
            chatServer = new System.Net.Sockets.TcpListener(4296);

            while (true)
            {
                chatServer.Start();
                if (chatServer.Pending())
                {
                    Chat.Sockets.TcpClient chatConnction = chatServer.AcceptTcpClient();
                    Console.WriteLine("you are now connected");
                    DoCommunicate comm = new DoCommunicate(chatConnction);
                }
            }
        }

        public static void SendMsgToAll(string nick, string msg)
        {
            StreamWriter writer;
            ArrayList toRemove = new ArrayList(0);

            Chat.Sockets.TcpClient[] tcpClient = new Chat.Sockets.TcpClient[ChatServer.nickName.Count];
            ChatServer.nickName.Values.CopyTo(tcpClient, 0);

            for (int cnt = 0; cnt < tcpClient.Length; cnt++)
            {
                try
                {
                    if (msg.Trim() == "" || tcpClient[cnt] == null)
                        continue;
                    writer = new StreamWriter(tcpClient[cnt].GetStream());
                    writer.WriteLine(nick + ": " + msg);
                    writer.Flush();
                    writer = null;
                }
                catch (Exception e44)
                {
                    e44 = e44;
                    string str = (string)ChatServer.nickNameByConnect[tcpClient[cnt]];
                    ChatServer.SendSysMsg("**" + str + "** has left the room.");
                    ChatServer.nickName.Remove(str);
                    ChatServer.nickNameByConnect.Remove(tcpClient[cnt]);
                }

            }
        }

        public static void SendSysMsg(string msg)
        {
            StreamWriter writer;
            ArrayList toRemove = new ArrayList(0);

            Chat.Sockets.TcpClient[] tcpClient = new Chat.Sockets.TcpClient[ChatServer.nickName.Count];
            ChatServer.nickName.Values.CopyTo(tcpClient, 0);

            for (int i = 0; i < tcpClient.Length; i++)
            {
                try
                {
                    if (msg.Trim() == "" || tcpClient[i] == null)
                        continue;
                    writer = new StreamWriter(tcpClient[i].GetStream());
                    writer.WriteLine(msg);
                    writer.Flush();
                    writer = null;
                }
                catch (Exception e44)
                {
                    e44 = e44;
                    
                    ChatServer.nickName.Remove(ChatServer.nickNameByConnect[tcpClient[i]]);
                    ChatServer.nickNameByConnect.Remove(tcpClient[i]);
                }

            }
        }

    }
}
