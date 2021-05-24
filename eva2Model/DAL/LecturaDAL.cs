using eva2Model.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace eva2Model.DAL
{
    public class LecturaDAL : ILecturaDAL
    {
        // Se puede resumir el código usando 1 solo obtenerlecturas que reciba un argumento tipo Medidor.Tipo (1,2) y utilizando un mismo archivo para las lecturas (lecturas.txt)
        // Al obtener lecturas y deserealizar se puede diferenciar por tipo para devolverlo en la lista. (lectura.Tipo == Medidor.Tipo.Consumo)

        
        private string archivoTrafico = Directory.GetCurrentDirectory()
        + Path.DirectorySeparatorChar + "lecturas_trafico.txt";

        private string archivoConsumo = Directory.GetCurrentDirectory()
        + Path.DirectorySeparatorChar + "lecturas_consumo.txt";
        private LecturaDAL() {}

        private static ILecturaDAL instancia;
        public static ILecturaDAL GetInstance()
        {
            if (instancia == null)
                instancia = new LecturaDAL();
            return instancia;
        }
        public List<Lectura> ObtenerLecturasConsumo()
        {
            List<Lectura> lecturas = new List<Lectura>();
            using (StreamReader file = new StreamReader(archivoConsumo))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    Lectura lectura = JsonSerializer.Deserialize<Lectura>(ln);
                    lecturas.Add(lectura);
                }
                file.Close();
            }

            return lecturas;
        }

        public List<Lectura> ObtenerLecturasTrafico()
        {
            List<Lectura> lecturas = new List<Lectura>();
            using (StreamReader file = new StreamReader(archivoTrafico))
            {
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    Lectura lectura = JsonSerializer.Deserialize<Lectura>(ln);
                    lecturas.Add(lectura);
                }
                file.Close();
            }

            return lecturas;
        }

        public void RegistrarLectura(Lectura lectura)
        {
            String jsonString = JsonSerializer.Serialize(lectura);
            if (lectura.Tipo == Medidor.Tipo.Consumo) {
                File.AppendAllText(archivoConsumo, jsonString + "\n");
            } else if (lectura.Tipo == Medidor.Tipo.Trafico)
            {
                File.AppendAllText(archivoTrafico, jsonString + "\n");
            } else
            {
                throw new Exception("Tipo invalido");
            }

        }
    }
}
