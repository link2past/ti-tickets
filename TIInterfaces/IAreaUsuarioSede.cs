using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface IAreaUsuarioSede
    {
        IList<AreaUsuarioSedeInfo> Listar(AreaUsuarioSedeInfo oAreaUsuarioSede);
        AreaUsuarioSedeInfo Consultar(AreaUsuarioSedeInfo oAreaUsuarioSede);
        Boolean Registrar(AreaUsuarioSedeInfo oAreaUsuarioSede, ref int? nId);
        Boolean Actualizar(AreaUsuarioSedeInfo oAreaUsuarioSede);
    }
}
