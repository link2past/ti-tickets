using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface IUsuario
    {
        Boolean ValidarAcceso(UsuarioInfo oUsuario);
        UsuarioInfo Consultar(UsuarioInfo oUsuario);
        IList<UsuarioInfo> Listar(UsuarioInfo oUsuario);
        Boolean Registrar(UsuarioInfo oUsuario);
        Boolean Actualizar(UsuarioInfo oUsuario);
        IList<UsuarioInfo> ListarTecnicosLibres(UsuarioInfo oUsuario);
    }
}
