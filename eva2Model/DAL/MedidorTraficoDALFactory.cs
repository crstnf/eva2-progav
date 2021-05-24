using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2Model.DAL
{
    public class MedidorTraficoDALFactory
    {
        public static IMedidorDAL CreateDAL()
        {
            return MedidorTraficoDAL.GetInstance();
        }
    }
}
