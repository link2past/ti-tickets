using System;

namespace TIEntidades
{
    [Serializable]
    public class TicketDetalleInfo
    {
        private int? _nroTicket;
        private int? _idRepuesto;
        private RepuestoInfo _repuesto;
        private Double? _cantidad;
        private String _idMoneda;
        private MonedaInfo _moneda;
        private Double? _precio;
        private String _usuarioCreacion;
        private String _usuarioModificacion;

        public TicketDetalleInfo()
        {
        }

        public TicketDetalleInfo(int? nNroTicket, int? nIdRepuesto, RepuestoInfo oRepuesto, Double? nCantidad,
                                 String sIdMoneda, MonedaInfo oMoneda, Double? nPrecio)
        {
            _nroTicket = nNroTicket;
            _idRepuesto = nIdRepuesto;
            _repuesto = oRepuesto;
            _cantidad = nCantidad;
            _idMoneda = sIdMoneda;
            _moneda = oMoneda;
            _precio = nPrecio;
        }

        public int? NroTicket
        {
            get { return _nroTicket; }
            set { _nroTicket = value; }
        }

        public int? IdRepuesto
        {
            get { return _idRepuesto; }
            set { _idRepuesto = value; }
        }

        public RepuestoInfo Repuesto
        {
            get { return _repuesto; }
            set { _repuesto = value; }
        }

        public double? Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
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

        public double? Precio
        {
            get { return _precio; }
            set { _precio = value; }
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
