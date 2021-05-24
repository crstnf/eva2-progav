using SocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eva2.Threads
{
    public class ServerThread
    {
        private int port;
        private ServerSocket server;
        public ServerThread(int port)
        {
            this.port = port;
        }

        public void Run()
        {
            Console.WriteLine("Starting server at port {0}", port);
            this.server = new ServerSocket(port);
            if (this.server.Run())
            {
                Console.WriteLine("Server started");

                while (true)
                {
                    ClientSocket client = this.server.GetClient();
                    ClientThread clientThread = new ClientThread(client);
                    Thread t = new Thread(new ThreadStart(clientThread.Run));
                    t.IsBackground = false;
                    t.Start();
                }

            }
        }
    }
}
