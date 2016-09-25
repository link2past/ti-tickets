using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface ITicketRegistro
    {
        IList<TicketRegistroInfo> Listar(TicketRegistroInfo oTicketRegistro);
        TicketRegistroInfo Consultar(TicketRegistroInfo oTicketRegistro);
        Boolean Registrar(TicketRegistroInfo oTicketRegistro);
        Boolean ActualizarHoraRegistro(TicketRegistroInfo oTicketRegistro);
    }
}
