using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class AreaUsuarioSede
    {
        private static readonly TIInterfaces.IAreaUsuarioSede DalAreaUsuarioSede = new TIAccesoDatos.AreaUsuarioSede();

        public IList<AreaUsuarioSedeInfo> Listar(AreaUsuarioSedeInfo oAreaUsuarioSede)
        {
            return DalAreaUsuarioSede.Listar(oAreaUsuarioSede);
        }

        public AreaUsuarioSedeInfo Consultar(AreaUsuarioSedeInfo oAreaUsuarioSede)
        {
            return DalAreaUsuarioSede.Consultar(oAreaUsuarioSede);
        }

        public bool Registrar(AreaUsuarioSedeInfo oAreaUsuarioSede, ref int? nId)
        {
            return DalAreaUsuarioSede.Registrar(oAreaUsuarioSede, ref nId);
        }

        public bool Actualizar(AreaUsuarioSedeInfo oAreaUsuarioSede)
        {
            return DalAreaUsuarioSede.Actualizar(oAreaUsuarioSede);
        }
    }
}
