using System;

namespace TIEntidades
{
    [Serializable]
    public class ClienteInfo
    {
        private int? _idCliente;
        private String _razonSocial;
        private String _nroDi;
        private String _direccion;
        private String _idDepartamento;
        private UbigeoInfo _departamento;
        private String _idProvincia;
        private UbigeoInfo _provincia;
        private String _idDistrito;
        private UbigeoInfo _distrito;
        private String _telefono;
        private String _email;
        private String _nombreContacto;
        private String _cargoContacto;
        private int? _idEstado;
        private EstadoInfo _estado;
        private String _usuarioCreacion;
        private String _usuarioModificacion;
        private String _idMoneda;
        private MonedaInfo _moneda;
        private Double? _tarifaDiurna;
        private Double? _tarifaNocturna;

        public ClienteInfo()
        {
        }

        public ClienteInfo(int? nIdCliente, String sRazonSocial, String sNroDi, String sDireccion,
                           String sIdDepartamento, UbigeoInfo oDepartamento,
                           String sIdProvincia, UbigeoInfo oProvincia, String sIdDistrito, UbigeoInfo oDistrito,
                           String sTelefono, String sEmail, String sNombreContacto,
                           String sCargoContacto, int? nIdEstado, EstadoInfo oEstado, String sUsuarioCreacion,
                           String sUsuarioModificacion)
        {
            _idCliente = nIdCliente;
            _razonSocial = sRazonSocial;
            _nroDi = sNroDi;
            _direccion = sDireccion;
            _idDepartamento = sIdDepartamento;
            _departamento = oDepartamento;
            _idProvincia = sIdProvincia;
            _provincia = oProvincia;
            _idDistrito = sIdDistrito;
            _distrito = oDistrito;
            _telefono = sTelefono;
            _email = sEmail;
            _nombreContacto = sNombreContacto;
            _cargoContacto = sCargoContacto;
            _idEstado = nIdEstado;
            _estado = oEstado;
            _usuarioCreacion = sUsuarioCreacion;
            _usuarioModificacion = sUsuarioModificacion;
        }

        public int? IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        public string RazonSocial
        {
            get { return _razonSocial; }
            set { _razonSocial = value; }
        }

        public string NroDi
        {
            get { return _nroDi; }
            set { _nroDi = value; }
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

        public string Email
        {
            get { return _email; }
            set { _email = value; }
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

        public string IdMoneda
        {
            get { return _idMoneda; }
            set { _idMoneda = value; }
        }

        public MonedaInfo Moneda
        {
            get { return _moneda; }
            set { _moneda = value; }
        }

        public double? TarifaDiurna
        {
            get { return _tarifaDiurna; }
            set { _tarifaDiurna = value; }
        }

        public double? TarifaNocturna
        {
            get { return _tarifaNocturna; }
            set { _tarifaNocturna = value; }
        }
    }
}
