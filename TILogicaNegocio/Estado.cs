using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class Estado
    {
        private static readonly TIInterfaces.IEstado DalEstado = new TIAccesoDatos.Estado();

        public IList<EstadoInfo> Listar(EstadoInfo oEstado)
        {
            return DalEstado.Listar(oEstado);
        }

        public EstadoInfo Consultar(EstadoInfo oEstado)
        {
            return DalEstado.Consultar(oEstado);
        }

        //public bool Registrar(EstadoInfo oEstado, ref int? nId)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool Actualizar(EstadoInfo oEstado)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
