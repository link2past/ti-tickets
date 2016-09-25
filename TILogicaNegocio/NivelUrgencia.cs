using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class NivelUrgencia
    {
        private static readonly TIInterfaces.INivelUrgencia DalNivelUrgencia = new TIAccesoDatos.NivelUrgencia();

        public IList<NivelUrgenciaInfo> Listar(NivelUrgenciaInfo oNivelUrgencia)
        {
            return DalNivelUrgencia.Listar(oNivelUrgencia);
        }

        public NivelUrgenciaInfo Consultar(NivelUrgenciaInfo oNivelUrgencia)
        {
            return DalNivelUrgencia.Consultar(oNivelUrgencia);
        }

        public bool Registrar(NivelUrgenciaInfo oNivelUrgencia, ref int? nId)
        {
            return DalNivelUrgencia.Registrar(oNivelUrgencia, ref nId);
        }

        public bool Actualizar(NivelUrgenciaInfo oNivelUrgencia)
        {
            return DalNivelUrgencia.Actualizar(oNivelUrgencia);
        }
    }
}
