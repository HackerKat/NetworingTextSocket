using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ClientApp
{
    class Program
    {
        public static Form form;
        public static TextBox msgBox;
        public static TextBox inputBox;
        public static TcpClient tcpClient;


        static void Main(string[] args)
        {
            form = new Form();
            form.Text = "Chat";

            msgBox = new TextBox();
            msgBox.Dock = DockStyle.Fill;
            msgBox.Multiline = true;

            inputBox = new TextBox();
            inputBox.Dock = DockStyle.Bottom;
            inputBox.Multiline = true;

            form.Controls.Add(msgBox);
            form.Controls.Add(inputBox);

            form.FormClosing += Closing;
            inputBox.KeyUp += KeyUp;
            form.Show();

            tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 4296);
            //Thread chatThread = new Thread(new ThreadStart(run));
            //chatThread.Start();
            //while (true)
            //{
            //    Application.DoEvents();
            //}
            run();
        }

        private static void Closing(object s, CancelEventArgs e)
        {
            e.Cancel = false;
            Application.Exit();
        }

        private static void KeyUp(object s, KeyEventArgs e)
        {
            TextBox txtChat = (TextBox)s;
            if (txtChat.Lines.Length > 1)
            {
                StreamWriter writer = new StreamWriter(tcpClient.GetStream());
                writer.WriteLine(txtChat.Text);
                writer.Flush();
                txtChat.Text = "";
                txtChat.Lines = null;
            }
        }

        private static void run()
        {
            StreamReader reader = new StreamReader(tcpClient.GetStream());
            while (true)
            {
                Application.DoEvents();
                var tmp = reader.ReadLineAsync();
                if (tmp.IsCompleted)
                {
                    msgBox.AppendText(tmp.Result + "\r\n");
                    msgBox.SelectionStart = msgBox.Text.Length;
                }
            }
        }
    }
}
