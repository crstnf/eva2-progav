using eva2Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2Model.DAL
{
    public class MedidorTraficoDAL : IMedidorDAL
    {
        private static IMedidorDAL instancia;
        public static IMedidorDAL GetInstance()
        {
            if (instancia == null)
                instancia = new MedidorTraficoDAL();
            return instancia;
        }

        private List<Medidor> medidores = new List<Medidor>()
                    {
                        new Medidor(3, DateTime.Parse("05/15/2021 11:48:00 AM"), Medidor.Tipo.Trafico),
                        new Medidor(4, DateTime.Parse("05/15/2021 11:49:00 AM"), Medidor.Tipo.Trafico),
                    };
        public List<Medidor> ObtenerMedidores()
        {
            return medidores;
        }
    }
}
