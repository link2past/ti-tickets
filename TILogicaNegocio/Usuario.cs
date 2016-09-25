using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class Usuario
    {
        private static readonly TIInterfaces.IUsuario DalUsuario = new TIAccesoDatos.Usuario();

        public Boolean ValidarUsuario(UsuarioInfo oUsuario)
        {
            return DalUsuario.ValidarAcceso(oUsuario);
        }

        public UsuarioInfo Consultar(UsuarioInfo oUsuario)
        {
            return DalUsuario.Consultar(oUsuario);
        }

        public IList<UsuarioInfo> Listar(UsuarioInfo oUsuario)
        {
            return DalUsuario.Listar(oUsuario);
        }

        public bool Registrar(UsuarioInfo oUsuario)
        {
            return DalUsuario.Registrar(oUsuario);
        }

        public bool Actualizar(UsuarioInfo oUsuario)
        {
            return DalUsuario.Actualizar(oUsuario);
        }

        public IList<UsuarioInfo> ListarTecnicosLibres(UsuarioInfo oUsuario)
        {
            return DalUsuario.ListarTecnicosLibres(oUsuario);
        }
    }
}
