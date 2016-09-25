using System;

namespace TIEntidades
{
    [Serializable]
    public class UsuarioClienteInfo
    {
        private String _idUsuario;
        private UsuarioInfo _usuario;
        private int? _idCliente;
        private ClienteInfo _cliente;
        private int? _idEstado;
        private EstadoInfo _estado;
        private String _usuarioCreacion;
        private String _usuarioModificacion;

        public UsuarioClienteInfo()
        {
        }

        public UsuarioClienteInfo(String sIdUsuario, int? nIdCliente, int? nIdEstado)
        {
            _idUsuario = sIdUsuario;
            _idCliente = nIdCliente;
            _idEstado = nIdEstado;
        }

        public string IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public UsuarioInfo Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public int? IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        public ClienteInfo Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
        }

        public int? IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        public string UsuarioCreacion
        {
            get { return _usuarioCreacion; }
            set { _usuarioCreacion = value; }
        }

        public string UsuarioModificacion
        {
            get { return _usuarioModificacion; }
            set { _usuarioModificacion = value; }
        }

        public EstadoInfo Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
    }
}
