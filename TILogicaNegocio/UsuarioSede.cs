using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class UsuarioSede
    {
        private static readonly TIInterfaces.IUsuarioSede DalUsuarioSede = new TIAccesoDatos.UsuarioSede();

        public IList<UsuarioSedeInfo> Listar(UsuarioSedeInfo oUsuarioSede)
        {
            return DalUsuarioSede.Listar(oUsuarioSede);
        }

        public UsuarioSedeInfo Consultar(UsuarioSedeInfo oUsuarioSede)
        {
            return DalUsuarioSede.Consultar(oUsuarioSede);
        }

        public bool Registrar(UsuarioSedeInfo oUsuarioSede, ref int? nId)
        {
            return DalUsuarioSede.Registrar(oUsuarioSede, ref nId);
        }

        public bool Actualizar(UsuarioSedeInfo oUsuarioSede)
        {
            return DalUsuarioSede.Actualizar(oUsuarioSede);
        }
    }
}
