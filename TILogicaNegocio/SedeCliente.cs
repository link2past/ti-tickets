using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class SedeCliente
    {
        private static readonly TIInterfaces.ISedeCliente DalSedeCliente = new TIAccesoDatos.SedeCliente();

        public IList<SedeClienteInfo> Listar(SedeClienteInfo oSedeCliente)
        {
            return DalSedeCliente.Listar(oSedeCliente);
        }

        public SedeClienteInfo Consultar(SedeClienteInfo oSedeCliente)
        {
            return DalSedeCliente.Consultar(oSedeCliente);
        }

        public bool Registrar(SedeClienteInfo oSedeCliente, ref int? nId)
        {
            return DalSedeCliente.Registrar(oSedeCliente, ref nId);
        }

        public bool Actualizar(SedeClienteInfo oSedeCliente)
        {
            return DalSedeCliente.Actualizar(oSedeCliente);
        }
    }
}
