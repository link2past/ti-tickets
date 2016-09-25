using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class TicketDetalle
    {
        private static readonly TIInterfaces.ITicketDetalle DalTicketDetalle = new TIAccesoDatos.TicketDetalle();

        public List<TicketDetalleInfo> Listar(TicketDetalleInfo oTicketDetalle)
        {
            return DalTicketDetalle.Listar(oTicketDetalle);
        }

        public bool Registrar(TicketDetalleInfo oTicketDetalle)
        {
            return DalTicketDetalle.Registrar(oTicketDetalle);
        }

        public bool Actualizar(TicketDetalleInfo oTicketDetalle)
        {
            return DalTicketDetalle.Actualizar(oTicketDetalle);
        }

        public bool Eliminar(TicketDetalleInfo oTicketDetalle)
        {
            return DalTicketDetalle.Eliminar(oTicketDetalle);
        }
    }
}
