using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface IEstadoTicket
    {
        IList<EstadoTicketInfo> Listar(EstadoTicketInfo oEstadoTicket);
        EstadoTicketInfo Consultar(EstadoTicketInfo oEstadoTicket);
        Boolean Registrar(EstadoTicketInfo oEstadoTicket, ref int? nId);
        Boolean Actualizar(EstadoTicketInfo oEstadoTicket);
    }
}
