using System;
using System.Collections.Generic;
using TIEntidades;

namespace TIInterfaces
{
    public interface ITicketDetalle
    {
        List<TicketDetalleInfo> Listar(TicketDetalleInfo oTicketDetalle);
        Boolean Registrar(TicketDetalleInfo oTicketDetalle);
        Boolean Actualizar(TicketDetalleInfo oTicketDetalle);
        Boolean Eliminar(TicketDetalleInfo oTicketDetalle);
    }
}
