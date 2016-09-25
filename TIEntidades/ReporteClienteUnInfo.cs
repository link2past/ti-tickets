using System;

namespace TIEntidades
{
    [Serializable]
    public class ReporteClienteUnInfo
    {
        private int? _nroTicket;
        private int? _idCliente;
        private int? _idSede;
        private int? _idUnidadNegocio;
        private TicketInfo _ticket;
        private DateTime? _fechaDesde;
        private DateTime? _fechaHasta;
        private TicketDetalleInfo _detalleTicket;
        private String _usuarioTicket;
        private DateTime? _fechaHoraRegistro;
        private Double? _precioTotal;
        private Double? _tarifa;
        private Double? _totalRepuestos;
        private int? _idEstadoTicket;
        private int? _idCategoriaProblema;
        private Double? _totalGeneral;

        public ReporteClienteUnInfo()
        {
            
        }

        public ReporteClienteUnInfo(int? nNroTicket, int? nIdCliente, int? nIdSede, int? nIdUnidadNegocio,
                                    DateTime? dFechaDesde, DateTime? dFechaHasta)
        {
            _nroTicket = nNroTicket;
            _idCliente = nIdCliente;
            _idSede = nIdSede;
            _idUnidadNegocio = nIdUnidadNegocio;
            _fechaDesde = dFechaDesde;
            _fechaHasta = dFechaHasta;
        }

        public int? NroTicket
        {
            get { return _nroTicket; }
            set { _nroTicket = value; }
        }

        public int? IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }

        public int? IdSede
        {
            get { return _idSede; }
            set { _idSede = value; }
        }

        public int? IdUnidadNegocio
        {
            get { return _idUnidadNegocio; }
            set { _idUnidadNegocio = value; }
        }

        public TicketInfo Ticket
        {
            get { return _ticket; }
            set { _ticket = value; }
        }

        public DateTime? FechaDesde
        {
            get { return _fechaDesde; }
            set { _fechaDesde = value; }
        }

        public DateTime? FechaHasta
        {
            get { return _fechaHasta; }
            set { _fechaHasta = value; }
        }

        public TicketDetalleInfo DetalleTicket
        {
            get { return _detalleTicket; }
            set { _detalleTicket = value; }
        }

        public string UsuarioTicket
        {
            get { return _usuarioTicket; }
            set { _usuarioTicket = value; }
        }

        public DateTime? FechaHoraRegistro
        {
            get { return _fechaHoraRegistro; }
            set { _fechaHoraRegistro = value; }
        }

        public double? PrecioTotal
        {
            get { return _precioTotal; }
            set { _precioTotal = value; }
        }

        public double? Tarifa
        {
            get { return _tarifa; }
            set { _tarifa = value; }
        }

        public double? TotalRepuestos
        {
            get { return _totalRepuestos; }
            set { _totalRepuestos = value; }
        }

        public int? IdEstadoTicket
        {
            get { return _idEstadoTicket; }
            set { _idEstadoTicket = value; }
        }

        public int? IdCategoriaProblema
        {
            get { return _idCategoriaProblema; }
            set { _idCategoriaProblema = value; }
        }

        public double? TotalGeneral
        {
            get { return _totalGeneral; }
            set { _totalGeneral = value; }
        }
    }
}
