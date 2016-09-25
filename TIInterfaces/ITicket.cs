using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface ITicket
    {
        IList<TicketInfo> Listar(TicketInfo oTicket);
        TicketInfo Consultar(TicketInfo oTicket);
        Boolean Registrar(TicketInfo oTicket, ref int? nId);
        Boolean Actualizar(TicketInfo oTicket);
    }
}
