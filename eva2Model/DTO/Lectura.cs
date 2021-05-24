using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2Model.DTO
{
    public class Lectura
    {
        public enum EstadoLectura
        {
            errorDeLectura = -1,
            OK = 0,
            puntoDeCargaLleno = 1,
            RequiereMantencion = 2
        }

        private int medidorID;
        private DateTime fecha;
        private int valor;
        private Medidor.Tipo tipo;
        private String unidadMedida;
        private EstadoLectura estado;

        public DateTime Fecha { get => fecha; set => fecha = value; }
        public int Valor { get => valor; set => valor = value; }
        public string UnidadMedida { get => unidadMedida; set => unidadMedida = value; }
        public Medidor.Tipo Tipo { get => tipo; set => tipo = value; }
        public EstadoLectura Estado { get => estado; set => estado = value; }
        public int MedidorID { get => medidorID; set => medidorID = value; }
    }
}
