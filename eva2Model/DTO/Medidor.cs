using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2Model.DTO
{
    public class Medidor
    {
        public enum Tipo
        {
            Desconocido = 0,
            Consumo = 1,
            Trafico = 2
        }

        private int id;
        private DateTime fechaInstalacion;
        private Tipo tipo;

        public Medidor(int id, DateTime fechaInstalacion, Tipo tipo)
        {
            this.id = id;
            this.fechaInstalacion = fechaInstalacion;
            this.tipo = tipo;
        }

        public int Id { get => id; set => id = value; }
        public DateTime FechaInstalacion { get => fechaInstalacion; set => fechaInstalacion = value; }
    }
}
