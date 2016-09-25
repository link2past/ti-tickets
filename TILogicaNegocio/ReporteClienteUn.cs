using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class ReporteClienteUn
    {
        private static readonly TIInterfaces.IReporteClienteUn DalReporteClienteUn = new TIAccesoDatos.ReporteClienteUn();
        
        public IList<ReporteClienteUnInfo> Procesar(ReporteClienteUnInfo oReporte)
        {
            return DalReporteClienteUn.Procesar(oReporte);
        }

        public IList<ReporteClienteUnInfo> ProcesarDetalle(ReporteClienteUnInfo oReporte)
        {
            return DalReporteClienteUn.ProcesarDetalle(oReporte);
        }

        public IList<ReporteClienteUnInfo> Procesar2(ReporteClienteUnInfo oReporte)
        {
            return DalReporteClienteUn.Procesar2(oReporte);
        }
    }
}
