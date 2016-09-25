using System;
using System.Collections.Generic;

namespace TIEntidades
{
    [Serializable]
    public class TicketInfo
    {
        private int? _nroTicket;
        private int? _idCliente;
        private ClienteInfo _cliente;
        private int? _idSede;
        private SedeClienteInfo _sede;
        private DateTime? _fechaTicket;
        private int? _idCategoriaProblema;
        private CategoriaProblemaInfo _categoriaProblema;
        private int? _idNivelUrgencia;
        private NivelUrgenciaInfo _nivelUrgencia;
        private String _titulo;
        private String _detalle;
        private String _solucion;
        private String _observaciones;
        private IList<TicketDetalleInfo> _detalleTicket;
        private IList<TicketRegistroInfo> _registrosTicket;
        private DateTime? _fechaDesde;
        private DateTime? _fechaHasta;
        private int? _idEstadoTicket;
        private EstadoTicketInfo _estadoTicket;
        private String _idUsuario;
        private String _tiempoTranscurrido;
        private String _usuarioAsignado;
        private String _nroTicketCliente;
        private int? _idUsuarioSede;
        private UsuarioSedeInfo _usuarioSede;
        private String _idUsuarioAsignado;
        private String _ordenServicio;
        private String _costoCero;
        private String _idMoneda;
        private Double? _tarifa;

        public TicketInfo()
        {
        }

        public TicketInfo(int? nNroTicket, int? nIdCliente, int? nIdSede, int? nIdCategoriaProblema, int? nIdNivelUrgencia, DateTime? dFechaDesde, DateTime? dFechaHasta, int? nIdEstadoTicket)
        {
            _nroTicket = nNroTicket;
            _idCliente = nIdCliente;
            _idSede = nIdSede;
            _idCategoriaProblema = nIdCategoriaProblema;
            _idNivelUrgencia = nIdNivelUrgencia;
            _fechaDesde = dFechaDesde;
            _fechaHasta = dFechaHasta;
            _idEstadoTicket = nIdEstadoTicket;
        }

        public int? NroTicket
        {
            get { return _nroTicket; }
            set { _nroTicket = value; }
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

        public DateTime? FechaTicket
        {
            get { return _fechaTicket; }
            set { _fechaTicket = value; }
        }

        public int? IdCategoriaProblema
        {
            get { return _idCategoriaProblema; }
            set { _idCategoriaProblema = value; }
        }

        public CategoriaProblemaInfo CategoriaProblema
        {
            get { return _categoriaProblema; }
            set { _categoriaProblema = value; }
        }

        public int? IdNivelUrgencia
        {
            get { return _idNivelUrgencia; }
            set { _idNivelUrgencia = value; }
        }

        public NivelUrgenciaInfo NivelUrgencia
        {
            get { return _nivelUrgencia; }
            set { _nivelUrgencia = value; }
        }

        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public string Detalle
        {
            get { return _detalle; }
            set { _detalle = value; }
        }

        public string Solucion
        {
            get { return _solucion; }
            set { _solucion = value; }
        }

        public string Observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
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

        public IList<TicketDetalleInfo> DetalleTicket
        {
            get { return _detalleTicket; }
            set { _detalleTicket = value; }
        }

        public IList<TicketRegistroInfo> RegistrosTicket
        {
            get { return _registrosTicket; }
            set { _registrosTicket = value; }
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

        public int? IdEstadoTicket
        {
            get { return _idEstadoTicket; }
            set { _idEstadoTicket = value; }
        }

        public EstadoTicketInfo EstadoTicket
        {
            get { return _estadoTicket; }
            set { _estadoTicket = value; }
        }

        public string IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }

        public String TiempoTranscurrido
        {
            get { return _tiempoTranscurrido; }
            set { _tiempoTranscurrido = value; }
        }

        public string UsuarioAsignado
        {
            get { return _usuarioAsignado; }
            set { _usuarioAsignado = value; }
        }

        public string NroTicketCliente
        {
            get { return _nroTicketCliente; }
            set { _nroTicketCliente = value; }
        }

        public int? IdUsuarioSede
        {
            get { return _idUsuarioSede; }
            set { _idUsuarioSede = value; }
        }

        public UsuarioSedeInfo UsuarioSede
        {
            get { return _usuarioSede; }
            set { _usuarioSede = value; }
        }

        public String IdUsuarioAsignado
        {
            get { return _idUsuarioAsignado; }
            set { _idUsuarioAsignado = value; }
        }

        public string OrdenServicio
        {
            get { return _ordenServicio; }
            set { _ordenServicio = value; }
        }

        public string CostoCero
        {
            get { return _costoCero; }
            set { _costoCero = value; }
        }

        public string IdMoneda
        {
            get { return _idMoneda; }
            set { _idMoneda = value; }
        }

        public double? Tarifa
        {
            get { return _tarifa; }
            set { _tarifa = value; }
        }
    }
}