﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.ComponentModel;

namespace ChatProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            new ChatServer();
        }
    }
}
