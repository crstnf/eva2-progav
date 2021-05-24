using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2Model.DTO
{
    public class ZonaHoraria
    {
        private String codigo;
        private String nombreLargo;

        public string Codigo { get => codigo; set => codigo = value; }
        public string NombreLargo { get => nombreLargo; set => nombreLargo = value; }
    }
}
