using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace eva2Cliente.SocketLogic
{
    public class SocketClient
    {
        private int puerto;
        private string ip;
        private Socket comServidor;
        private StreamReader reader;
        private StreamWriter writer;

        public SocketClient(int puerto, string ip)
        {
            this.puerto = puerto;
            this.ip = ip;
        }

        public bool Conectar()
        {
            try
            {
                this.comServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), puerto);
                this.comServidor.Connect(endpoint);
                Stream stream = new NetworkStream(this.comServidor);
                this.writer = new StreamWriter(stream);
                this.reader = new StreamReader(stream);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
                return true;
            }
            catch (IOException ex)
            {
                return false;
            }
        }

        public string Leer()
        {
            try
            {
                return this.reader.ReadLine().Trim();
            }
            catch (IOException ex)
            {
                return null;
            }
        }

        public void Desconectar()
        {
            this.comServidor.Close();
        }

    }
}
