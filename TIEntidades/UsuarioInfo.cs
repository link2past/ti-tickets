using System;

namespace TIEntidades
{
    [Serializable]
    public class UsuarioInfo
    {
        private String _usuario;
        private String _contraseña;
        private String _nombre;
        private DateTime? _fechaCreacion;
        private Int32? _diasSolicitudCambio;
        private DateTime? _fechaUltimoCambio;
        private String _requiereCambioClave;
        private int? _idEstado;
        private EstadoInfo _estado;
        private int? _idTipoUsuario;
        private TipoUsuarioInfo _tipoUsuario;
        private String _email;

        public UsuarioInfo()
        {
        }

        public UsuarioInfo(String sUsuario, String sContraseña, String sNombre, int? nIdTipoUsuario)
        {
            Usuario = sUsuario;
            Contraseña = sContraseña;
            Nombre = sNombre;
            _idTipoUsuario = nIdTipoUsuario;
        }

        public UsuarioInfo(String sUsuario, String sContraseña, String sNombre, DateTime? dFechaCreacion, Int32? nDiasSolicitudCambio, String sRequiereCambioClave,
            DateTime? dFechaUltimoCambio, int? nIdEstado, EstadoInfo oEstado, int? nIdTipoUsuario, TipoUsuarioInfo oTipoUsuario, String sEmail)
        {
            _usuario = sUsuario;
            _contraseña = sContraseña;
            _nombre = sNombre;
            _fechaCreacion = dFechaCreacion;
            _diasSolicitudCambio = nDiasSolicitudCambio;
            _requiereCambioClave = sRequiereCambioClave;
            _fechaUltimoCambio = dFechaUltimoCambio;
            _idEstado = nIdEstado;
            _estado = oEstado;
            _idTipoUsuario = nIdTipoUsuario;
            _tipoUsuario = oTipoUsuario;
            _email = sEmail;
        }

        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string Contraseña
        {
            get { return _contraseña; }
            set { _contraseña = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public DateTime? FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }

        public int? DiasSolicitudCambio
        {
            get { return _diasSolicitudCambio; }
            set { _diasSolicitudCambio = value; }
        }

        public DateTime? FechaUltimoCambio
        {
            get { return _fechaUltimoCambio; }
            set { _fechaUltimoCambio = value; }
        }

        public string RequiereCambioClave
        {
            get { return _requiereCambioClave; }
            set { _requiereCambioClave = value; }
        }

        public int? IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }

        public EstadoInfo Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public int? IdTipoUsuario
        {
            get { return _idTipoUsuario; }
            set { _idTipoUsuario = value; }
        }

        public TipoUsuarioInfo TipoUsuario
        {
            get { return _tipoUsuario; }
            set { _tipoUsuario = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}
