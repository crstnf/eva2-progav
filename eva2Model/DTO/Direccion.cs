using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2Model.DTO
{
    public class Direccion
    {
        private int codPostal;
        private String detalle;
        private int nro;
        private String adicional;

        public int CodPostal { get => codPostal; set => codPostal = value; }
        public string Detalle { get => detalle; set => detalle = value; }
        public int Nro { get => nro; set => nro = value; }
        public string Adicional { get => adicional; set => adicional = value; }
    }
}
