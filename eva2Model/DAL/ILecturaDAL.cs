using eva2Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2Model.DAL
{
    public interface ILecturaDAL
    {
        void RegistrarLectura(Lectura lectura);
        List<Lectura> ObtenerLecturasTrafico();
        List<Lectura> ObtenerLecturasConsumo();
    }
}
