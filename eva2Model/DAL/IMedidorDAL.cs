using eva2Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2Model.DAL
{
    public interface IMedidorDAL
    {
        List<Medidor> ObtenerMedidores();
    }
}
