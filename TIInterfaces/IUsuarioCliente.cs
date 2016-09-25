using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface IUsuarioCliente
    {
        IList<UsuarioClienteInfo> Listar(UsuarioClienteInfo oUsuarioCliente);
        IList<UsuarioClienteInfo> ListarPendientes();
        UsuarioClienteInfo Consultar(UsuarioClienteInfo oUsuarioCliente);
        Boolean Registrar(UsuarioClienteInfo oUsuarioCliente);
        Boolean Actualizar(UsuarioClienteInfo oUsuarioCliente);
    }
}
