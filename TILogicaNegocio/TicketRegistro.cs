using System.Collections.Generic;
using TIEntidades;

namespace TILogicaNegocio
{
    public class TicketRegistro
    {
        private static readonly TIInterfaces.ITicketRegistro DalTicketRegistro = new TIAccesoDatos.TicketRegistro();

        public IList<TicketRegistroInfo> Listar(TicketRegistroInfo oTicketRegistro)
        {
            return DalTicketRegistro.Listar(oTicketRegistro);
        }

        public TicketRegistroInfo Consultar(TicketRegistroInfo oTicketRegistro)
        {
            return DalTicketRegistro.Consultar(oTicketRegistro);
        }

        public bool Registrar(TicketRegistroInfo oTicketRegistro)
        {
            return DalTicketRegistro.Registrar(oTicketRegistro);
        }

        public bool ActualizarHoraRegistro(TicketRegistroInfo oTicketRegistro)
        {
            return DalTicketRegistro.ActualizarHoraRegistro(oTicketRegistro);
        }
    }
}
