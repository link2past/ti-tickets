using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class Cliente
    {
        private static readonly TIInterfaces.ICliente DalCliente = new TIAccesoDatos.Cliente();

        public IList<ClienteInfo> Listar(ClienteInfo oCliente)
        {
            return DalCliente.Listar(oCliente);
        }

        public ClienteInfo Consultar(ClienteInfo oCliente)
        {
            return DalCliente.Consultar(oCliente);
        }

        public bool Registrar(ClienteInfo oCliente, ref int? nId)
        {
            return DalCliente.Registrar(oCliente, ref nId);
        }

        public bool Actualizar(ClienteInfo oCliente)
        {
            return DalCliente.Actualizar(oCliente);
        }
    }
}
