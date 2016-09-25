using System;
using System.Collections.Generic;
using TIEntidades;
namespace TILogicaNegocio
{
    public class TipoUsuario
    {
        private static readonly TIInterfaces.ITipoUsuario DalTipoUsuario = new TIAccesoDatos.TipoUsuario();

        public IList<TipoUsuarioInfo> Listar(TipoUsuarioInfo oTipoUsuario)
        {
            return DalTipoUsuario.Listar(oTipoUsuario);
        }

        public TipoUsuarioInfo Consultar(TipoUsuarioInfo oTipoUsuario)
        {
            return DalTipoUsuario.Consultar(oTipoUsuario);
        }

        public bool Registrar(TipoUsuarioInfo oTipoUsuario, ref int? nId)
        {
            return DalTipoUsuario.Registrar(oTipoUsuario, ref nId);
        }

        public bool Actualizar(TipoUsuarioInfo oTipoUsuario)
        {
            return DalTipoUsuario.Actualizar(oTipoUsuario);
        }
    }
}
