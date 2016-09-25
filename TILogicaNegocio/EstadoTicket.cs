using System;
using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class EstadoTicket
    {
        private static readonly TIInterfaces.IEstadoTicket DalEstadoTicket = new TIAccesoDatos.EstadoTicket();

        public IList<EstadoTicketInfo> Listar(EstadoTicketInfo oEstadoTicket)
        {
            return DalEstadoTicket.Listar(oEstadoTicket);
        }

        public EstadoTicketInfo Consultar(EstadoTicketInfo oEstadoTicket)
        {
            return DalEstadoTicket.Consultar(oEstadoTicket);
        }

        public bool Registrar(EstadoTicketInfo oEstadoTicket, ref int? nId)
        {
            return DalEstadoTicket.Registrar(oEstadoTicket, ref nId);
        }

        public bool Actualizar(EstadoTicketInfo oEstadoTicket)
        {
            return DalEstadoTicket.Actualizar(oEstadoTicket);
        }
    }
}
