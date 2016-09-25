using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface ICliente
    {
        IList<ClienteInfo> Listar(ClienteInfo oCliente);
        ClienteInfo Consultar(ClienteInfo oCliente);
        Boolean Registrar(ClienteInfo oCliente, ref int? nId);
        Boolean Actualizar(ClienteInfo oCliente);
    }
}
