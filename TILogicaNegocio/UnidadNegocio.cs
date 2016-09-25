using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class UnidadNegocio
    {
        private static readonly TIInterfaces.IUnidadNegocio DalUnidadNegocio = new TIAccesoDatos.UnidadNegocio();

        public IList<UnidadNegocioInfo> Listar(UnidadNegocioInfo oUnidadNegocio)
        {
            return DalUnidadNegocio.Listar(oUnidadNegocio);
        }

        public UnidadNegocioInfo Consultar(UnidadNegocioInfo oUnidadNegocio)
        {
            return DalUnidadNegocio.Consultar(oUnidadNegocio);
        }

        public bool Registrar(UnidadNegocioInfo oUnidadNegocio, ref int? nId)
        {
            return DalUnidadNegocio.Registrar(oUnidadNegocio, ref nId);
        }

        public bool Actualizar(UnidadNegocioInfo oUnidadNegocio)
        {
            return DalUnidadNegocio.Actualizar(oUnidadNegocio);
        }

        public IList<UnidadNegocioInfo> ListarPorCliente(UnidadNegocioInfo oUnidadNegocio, int? nIdCliente)
        {
            return DalUnidadNegocio.ListarPorCliente(oUnidadNegocio, nIdCliente);
        }
    }
}
