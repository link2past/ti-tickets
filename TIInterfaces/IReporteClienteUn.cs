using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface IReporteClienteUn
    {
        IList<ReporteClienteUnInfo> Procesar(ReporteClienteUnInfo oReporte);
        IList<ReporteClienteUnInfo> Procesar2(ReporteClienteUnInfo oReporte);
        IList<ReporteClienteUnInfo> ProcesarDetalle(ReporteClienteUnInfo oReporte);
    }
}
