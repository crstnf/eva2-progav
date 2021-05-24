using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2Model.DTO
{
    public class TarifaElectrica
    {
        private String codigo;
        private int factor;

        public string Codigo { get => codigo; set => codigo = value; }
        public int Factor { get => factor; set => factor = value; }
    }
}
