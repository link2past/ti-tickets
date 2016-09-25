using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class UsuarioCliente
    {
        private static readonly TIInterfaces.IUsuarioCliente DalUsuarioCliente = new TIAccesoDatos.UsuarioCliente();

        public IList<UsuarioClienteInfo> Listar(UsuarioClienteInfo oUsuarioCliente)
        {
            return DalUsuarioCliente.Listar(oUsuarioCliente);
        }

        public UsuarioClienteInfo Consultar(UsuarioClienteInfo oUsuarioCliente)
        {
            return DalUsuarioCliente.Consultar(oUsuarioCliente);
        }

        public bool Registrar(UsuarioClienteInfo oUsuarioCliente)
        {
            return DalUsuarioCliente.Registrar(oUsuarioCliente);
        }

        public bool Actualizar(UsuarioClienteInfo oUsuarioCliente)
        {
            return DalUsuarioCliente.Actualizar(oUsuarioCliente);
        }

        public IList<UsuarioClienteInfo> ListarPendientes()
        {
            return DalUsuarioCliente.ListarPendientes();
        }
    }
}
