using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface IEstado
    {
        IList<EstadoInfo> Listar(EstadoInfo oEstado);
        EstadoInfo Consultar(EstadoInfo oEstado);
        Boolean Registrar(EstadoInfo oEstado, ref int? nId);
        Boolean Actualizar(EstadoInfo oEstado);
    }
}
