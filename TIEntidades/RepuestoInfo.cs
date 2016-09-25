using System;

namespace TIEntidades
{
    [Serializable]
    public class RepuestoInfo
    {
        private int? _idRepuesto;
        private String _descripcion;
        private String _idMoneda;
        private MonedaInfo _moneda;
        private Double? _precioActual;
        private Double? _stockActual;
        private String _usuarioCreacion;
        private String _usuarioModificacion;
        private int? _idEstado;
        private EstadoInfo _estado;

        public RepuestoInfo()
        {
        }

        public RepuestoInfo(int? nIdRepuesto, String sDescripcion, String sIdMoneda, MonedaInfo oMoneda,
                            Double? nPrecioActual, Double? nStockActual, String sUsuarioCreacion,
                            String sUsuarioModificacion, int? nIdEstado, EstadoInfo oEstado)
        {
            _idRepuesto = nIdRepuesto;
            _descripcion = sDescripcion;
            _idMoneda = sIdMoneda;
            _moneda = oMoneda;
            _precioActual = nPrecioActual;
            _stockActual = nStockActual;
            _usuarioCreacion = sUsuarioCreacion;
            _usuarioModificacion = sUsuarioModificacion;
            _idEstado = nIdEstado;
            _estado = oEstado;
        }

        public int? IdRepuesto
        {
            get { return _idRepuesto; }
            set { _idRepuesto = value; }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
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

        public double? PrecioActual
        {
            get { return _precioActual; }
            set { _precioActual = value; }
        }

        public double? StockActual
        {
            get { return _stockActual; }
            set { _stockActual = value; }
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
    }
}
