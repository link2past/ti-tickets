using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface INivelUrgencia
    {
        IList<NivelUrgenciaInfo> Listar(NivelUrgenciaInfo oNivelUrgencia);
        NivelUrgenciaInfo Consultar(NivelUrgenciaInfo oNivelUrgencia);
        Boolean Registrar(NivelUrgenciaInfo oNivelUrgencia, ref int? nId);
        Boolean Actualizar(NivelUrgenciaInfo oNivelUrgencia);
    }
}
