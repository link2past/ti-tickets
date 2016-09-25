using System;

namespace TIEntidades
{
    [Serializable]
    public class TicketRegistroInfo
    {
        private int? _nroTicket;
        private int? _idEstadoTicket;
        private EstadoTicketInfo _estadoTicket;
        private String _idUsuario;
        private UsuarioInfo _usuario;
        private DateTime? _fechaHoraRegistro;
        private String _idUsuarioAsignado;
        private UsuarioInfo _usuarioAsignado;
        private String _observacion;

        public TicketRegistroInfo()
        {
        }

        public TicketRegistroInfo(int? nNroTicket, int? nIdEstadoTicket, EstadoTicketInfo oEstadoTicket,
                                  String sIdUsuario, DateTime? dFechaHoraRegistro, String sIdUsuarioAsignado)
        {
            _nroTicket = nNroTicket;
            _idEstadoTicket = nIdEstadoTicket;
            _estadoTicket = oEstadoTicket;
            _idUsuario = sIdUsuario;
            _fechaHoraRegistro = dFechaHoraRegistro;
            _idUsuarioAsignado = sIdUsuarioAsignado;
        }

        public int? NroTicket
        {
            get { return _nroTicket; }
            set { _nroTicket = value; }
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

        public DateTime? FechaHoraRegistro
        {
            get { return _fechaHoraRegistro; }
            set { _fechaHoraRegistro = value; }
        }

        public UsuarioInfo Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string IdUsuarioAsignado
        {
            get { return _idUsuarioAsignado; }
            set { _idUsuarioAsignado = value; }
        }

        public UsuarioInfo UsuarioAsignado
        {
            get { return _usuarioAsignado; }
            set { _usuarioAsignado = value; }
        }

        public string Observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }
    }
}
