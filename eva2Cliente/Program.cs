using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eva2Cliente.SocketLogic;

namespace eva2Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = ConfigurationManager.AppSettings["ip"];
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);

            SocketClient SocketLogic = new SocketClient(puerto, ip);

           while (1==1)
           {
                Console.WriteLine("Tipo de medidor:\n1.- Consumo\n2.- Trafico\nOpcion: ");
                string opc = Console.ReadLine().Trim();

                string tipo = null;

                switch (opc)
                {
                    case "1":
                        tipo = "consumo";
                        break;

                    case "2":
                        tipo = "trafico";
                        break;

                    default:
                        break;
                }

                if (tipo == null) return;

                Console.WriteLine("Numero de serie: ");
                string nroSerie = Console.ReadLine().Trim();

                Console.WriteLine("Fecha: ");
                string fecha = Console.ReadLine().Trim();

                if (SocketLogic.Conectar())
                {
                    SocketLogic.Escribir(fecha + "|" + nroSerie + "|" + tipo);
                    string handshakeResponse = SocketLogic.Leer().Trim();

                    if (handshakeResponse.Split('|')[1] == "WAIT")
                    {
                        Console.WriteLine("El servidor espera una update");

                        Console.WriteLine("Valor: ");
                        string valor = Console.ReadLine().Trim();

                        Console.WriteLine("Estado: ");
                        string estado = Console.ReadLine().Trim();

                        SocketLogic.Escribir(nroSerie + "|" + fecha + "|" + tipo + "|" + valor + "|" + estado + "|UPDATE");
                        SocketLogic.Desconectar();
                    } else if (handshakeResponse.Split('|')[1] == "ERROR")
                    {
                        Console.WriteLine("Hubo un error de comunicacion");
                    }
                }
                else
                {
                    Console.WriteLine("Error al conectar al servidor");
                }
            }

        }
    }
}
