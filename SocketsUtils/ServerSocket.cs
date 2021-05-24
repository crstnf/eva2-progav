using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketUtils
{
    public class ServerSocket
    {
        private int port;
        private Socket servidor;

        public ServerSocket(int port)
        {
            this.port = port;
        }

        public bool Run()
        {
            try
            {
                this.servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.servidor.Bind(new IPEndPoint(IPAddress.Any, this.port));
                this.servidor.Listen(10);
                return true;
            } catch(Exception ex)
            {
                Console.WriteLine("got exception: " );
                return false;
            }

        }
       
        public ClientSocket GetClient()
        {
            try
            {
                return new ClientSocket(this.servidor.Accept());
            }catch(Exception ex)
            {
                Console.WriteLine("got exception: " );
                return null;
            }
        }
    }
}
