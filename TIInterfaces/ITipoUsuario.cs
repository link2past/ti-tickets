using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface ITipoUsuario
    {
        IList<TipoUsuarioInfo> Listar(TipoUsuarioInfo oTipoUsuario);
        TipoUsuarioInfo Consultar(TipoUsuarioInfo oTipoUsuario);
        Boolean Registrar(TipoUsuarioInfo oTipoUsuario, ref int? nId);
        Boolean Actualizar(TipoUsuarioInfo oTipoUsuario);
    }
}
