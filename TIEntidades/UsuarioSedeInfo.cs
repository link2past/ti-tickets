using System;

namespace TIEntidades
{
    [Serializable]
    public class UsuarioSedeInfo
    {
        private int? _idUsuarioSede;
        private String _nombre;
        private int? _idEstado;
        private EstadoInfo _estado;
        private int? _idAreaUsuarioSede;
        private int? _idCliente;
        private ClienteInfo _cliente;
        private int? _idSede;
        private SedeClienteInfo _sede;
        private AreaUsuarioSedeInfo _areaUsuarioSede;
        private String _usuarioCreacion;
        private String _usuarioModificacion;

        public UsuarioSedeInfo()
        {
            
        }

        public UsuarioSedeInfo(int? nIdUsuarioSede, String sNombre, int? nIdEstado, EstadoInfo oEstado,
                               int? nIdAreaUsuarioSede, AreaUsuarioSedeInfo oAreaUsuarioSede, int? nIdCliente, ClienteInfo oCliente, 
                                int? nIdSede, SedeClienteInfo oSede)
        {
            _idUsuarioSede = nIdUsuarioSede;
            _nombre = sNombre;
            _idEstado = nIdEstado;
            _estado = oEstado;
            _idAreaUsuarioSede = nIdAreaUsuarioSede;
            _areaUsuarioSede = oAreaUsuarioSede;
            _idCliente = nIdCliente;
            _cliente = oCliente;
            _idSede = nIdSede;
            _sede = oSede;
        }

        public int? IdUsuarioSede
        {
            get { return _idUsuarioSede; }
            set { _idUsuarioSede = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
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

        public int? IdAreaUsuarioSede
        {
            get { return _idAreaUsuarioSede; }
            set { _idAreaUsuarioSede = value; }
        }

        public AreaUsuarioSedeInfo AreaUsuarioSede
        {
            get { return _areaUsuarioSede; }
            set { _areaUsuarioSede = value; }
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

        public int? IdSede
        {
            get { return _idSede; }
            set { _idSede = value; }
        }

        public SedeClienteInfo Sede
        {
            get { return _sede; }
            set { _sede = value; }
        }
    }
}
