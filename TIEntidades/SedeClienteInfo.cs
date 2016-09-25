using System;

namespace TIEntidades
{
    [Serializable]
    public class SedeClienteInfo
    {
        private int? _idSedeCliente;
        private String _nombre;
        private int? _idCliente;
        private ClienteInfo _cliente;
        private int? _idUnidadNegocio;
        private UnidadNegocioInfo _unidadNegocio;
        private String _direccion;
        private String _idDepartamento;
        private UbigeoInfo _departamento;
        private String _idProvincia;
        private UbigeoInfo _provincia;
        private String _idDistrito;
        private UbigeoInfo _distrito;
        private String _telefono;
        private String _nombreContacto;
        private String _cargoContacto;
        private int? _idEstado;
        private EstadoInfo _estado;
        private String _centroCosto;
        private String _usuarioCreacion;
        private String _usuarioModificacion;

        public SedeClienteInfo()
        {
        }

        public SedeClienteInfo(int? nIdSedeCliente, int? nIdCliente, int? nIdEstado)
        {
            _idSedeCliente = nIdSedeCliente;
            _idCliente = nIdCliente;
            _idEstado = nIdEstado;
        }

        public int? IdSedeCliente
        {
            get { return _idSedeCliente; }
            set { _idSedeCliente = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
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

        public int? IdUnidadNegocio
        {
            get { return _idUnidadNegocio; }
            set { _idUnidadNegocio = value; }
        }

        public UnidadNegocioInfo UnidadNegocio
        {
            get { return _unidadNegocio; }
            set { _unidadNegocio = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public string IdDepartamento
        {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }

        public UbigeoInfo Departamento
        {
            get { return _departamento; }
            set { _departamento = value; }
        }

        public string IdProvincia
        {
            get { return _idProvincia; }
            set { _idProvincia = value; }
        }

        public UbigeoInfo Provincia
        {
            get { return _provincia; }
            set { _provincia = value; }
        }

        public string IdDistrito
        {
            get { return _idDistrito; }
            set { _idDistrito = value; }
        }

        public UbigeoInfo Distrito
        {
            get { return _distrito; }
            set { _distrito = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public string NombreContacto
        {
            get { return _nombreContacto; }
            set { _nombreContacto = value; }
        }

        public string CargoContacto
        {
            get { return _cargoContacto; }
            set { _cargoContacto = value; }
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

        public string CentroCosto
        {
            get { return _centroCosto; }
            set { _centroCosto = value; }
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
    }
}
