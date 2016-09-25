using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class Ubigeo
    {
        private static readonly TIInterfaces.IUbigeo DalUbigeo = new TIAccesoDatos.Ubigeo();

        public IList<UbigeoInfo> Listar(UbigeoInfo oUbigeo)
        {
            return DalUbigeo.Listar(oUbigeo);
        }

        //public UbigeoInfo Consultar(UbigeoInfo oUbigeo)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
