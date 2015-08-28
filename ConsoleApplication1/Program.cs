using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //  master branch
            IPEndPoint localpt = new IPEndPoint(IPAddress.Loopback, 6000);

            ThreadPool.QueueUserWorkItem(delegate
            {
                UdpClient udpServer = new UdpClient();
                udpServer.ExclusiveAddressUse = false;
                udpServer.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                udpServer.Client.Bind(localpt);

                IPEndPoint inEndPoint = new IPEndPoint(IPAddress.Any, 0);
                Console.WriteLine("Listening on " + localpt + ".");
                byte[] buffer = udpServer.Receive(ref inEndPoint);
                Console.WriteLine("Receive from " + inEndPoint + " " + Encoding.ASCII.GetString(buffer) + ".");
            });

            // Sleep time was inreased.
            Thread.Sleep(10000);

            Console.ReadKey();
        }
    }
}
