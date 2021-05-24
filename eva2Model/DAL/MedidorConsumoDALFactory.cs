using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2Model.DAL
{
    public class MedidorConsumoDALFactory
    {
        public static IMedidorDAL CreateDAL()
        {
            return MedidorConsumoDAL.GetInstance();
        }
    }
}
