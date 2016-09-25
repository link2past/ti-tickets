using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class Repuesto
    {
        private static readonly TIInterfaces.IRepuesto DalRepuesto = new TIAccesoDatos.Repuesto();

        public IList<RepuestoInfo> Listar(RepuestoInfo oRepuesto)
        {
            return DalRepuesto.Listar(oRepuesto);
        }

        public RepuestoInfo Consultar(RepuestoInfo oRepuesto)
        {
            return DalRepuesto.Consultar(oRepuesto);
        }

        public bool Registrar(RepuestoInfo oRepuesto, ref int? nId)
        {
            return DalRepuesto.Registrar(oRepuesto, ref nId);
        }

        public bool Actualizar(RepuestoInfo oRepuesto)
        {
            return DalRepuesto.Actualizar(oRepuesto);
        }
    }
}
