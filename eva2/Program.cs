using eva2.Threads;
using eva2Model.DAL;
using eva2Model.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eva2
{
    class Program
    {
        /*static private ILecturaDAL dal = LecturaDALFactory.CreateDAL();
        static private List<Lectura> lecturas;
        static bool menu()
        {
            bool continuar = true;
            Console.WriteLine("opc:");
            Console.WriteLine("1 med consumo");
            Console.WriteLine("2 med trafico");
            string opcion = Console.ReadLine().Trim();
            switch (opcion)
            {
                case "1":
                    lecturas = dal.ObtenerLecturasConsumo();
                    foreach (Lectura lectura in lecturas)
                    {
                        Console.WriteLine($"Fecha: {lectura.Fecha} / Valor: {lectura.Valor} / Tipo: {lectura.Tipo} / Unidad Medida: {lectura.UnidadMedida}");
                    }
                    break;
                case "2":
                    lecturas = dal.ObtenerLecturasTrafico();
                    foreach (Lectura lectura in lecturas)
                    {
                        Console.WriteLine($"Fecha: {lectura.Fecha} / Valor: {lectura.Valor} / Tipo: {lectura.Tipo} / Unidad Medida: {lectura.UnidadMedida}");
                    }
                    break;
                case "0":
                    continuar = false;
                    break;
            }
            return continuar;
        }*/
        static void Main(string[] args)
        {
            Console.WriteLine("Starting ServerThread");
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            ServerThread serverThread = new ServerThread(port);
            Thread t = new Thread(new ThreadStart(serverThread.Run));
            t.IsBackground = false;
            t.Start();
            //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
            //while (menu());
        }
    }
}
