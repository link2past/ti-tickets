using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class Ticket
    {
        private static readonly TIInterfaces.ITicket DalTicket = new TIAccesoDatos.Ticket();

        public IList<TicketInfo> Listar(TicketInfo oTicket)
        {
            return DalTicket.Listar(oTicket);
        }

        public TicketInfo Consultar(TicketInfo oTicket)
        {
            return DalTicket.Consultar(oTicket);
        }

        public bool Registrar(TicketInfo oTicket, ref int? nId)
        {
            return DalTicket.Registrar(oTicket, ref nId);
        }

        public bool Actualizar(TicketInfo oTicket)
        {
            return DalTicket.Actualizar(oTicket);
        }
    }
}
