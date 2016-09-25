using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class Moneda
    {
        private static readonly TIInterfaces.IMoneda DalMoneda = new TIAccesoDatos.Moneda();

        public IList<MonedaInfo> Listar(MonedaInfo oMoneda)
        {
            return DalMoneda.Listar(oMoneda);
        }

        public MonedaInfo Consultar(MonedaInfo oMoneda)
        {
            return DalMoneda.Consultar(oMoneda);
        }
    }
}
