using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface IRepuesto
    {
        IList<RepuestoInfo> Listar(RepuestoInfo oRepuesto);
        RepuestoInfo Consultar(RepuestoInfo oRepuesto);
        Boolean Registrar(RepuestoInfo oRepuesto, ref int? nId);
        Boolean Actualizar(RepuestoInfo oRepuesto);
    }
}
