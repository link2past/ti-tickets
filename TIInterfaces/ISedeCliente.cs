using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface ISedeCliente
    {
        IList<SedeClienteInfo> Listar(SedeClienteInfo oSedeCliente);
        SedeClienteInfo Consultar(SedeClienteInfo oSedeCliente);
        Boolean Registrar(SedeClienteInfo oSedeCliente, ref int? nId);
        Boolean Actualizar(SedeClienteInfo oSedeCliente);
    }
}
