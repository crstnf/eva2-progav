using eva2Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eva2Model.DAL
{
    public class MedidorConsumoDAL : IMedidorDAL
    {
        private static IMedidorDAL instancia;
        public static IMedidorDAL GetInstance()
        {
            if (instancia == null)
                instancia = new MedidorConsumoDAL();
            return instancia;
        }

        private List<Medidor> medidores = new List<Medidor>()
                    {
                        new Medidor(1, DateTime.Parse("05/15/2021 11:48:00 AM"), Medidor.Tipo.Consumo),
                        new Medidor(2, DateTime.Parse("05/15/2021 11:49:00 AM"), Medidor.Tipo.Consumo),
                    };
        public List<Medidor> ObtenerMedidores()
        {
            return medidores;
        }

    }
}
