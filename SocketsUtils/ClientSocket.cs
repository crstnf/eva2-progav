using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketUtils
{
    public class ClientSocket
    {
        private Socket comCliente;
        private StreamReader reader;
        private StreamWriter writer;

        public ClientSocket(Socket comCliente)
        {
            this.comCliente = comCliente;
            Stream stream = new NetworkStream(this.comCliente);
            this.writer = new StreamWriter(stream);
            this.reader = new StreamReader(stream);
        }

        public bool Write(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("got exception: " );
                return false;
            }
        }

        public string Read()
        {
            try
            {
                return this.reader.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("got exception: " );
                return null;
            }
        }

        public void CloseConnection()
        {
            this.comCliente.Close();
        }
    }
}
