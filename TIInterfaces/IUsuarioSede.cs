using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface IUsuarioSede
    {
        IList<UsuarioSedeInfo> Listar(UsuarioSedeInfo oUsuarioSede);
        UsuarioSedeInfo Consultar(UsuarioSedeInfo oUsuarioSede);
        Boolean Registrar(UsuarioSedeInfo oUsuarioSede, ref int? nId);
        Boolean Actualizar(UsuarioSedeInfo oUsuarioSede);
    }
}
