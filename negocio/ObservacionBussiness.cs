using acceso_datos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ObservacionBussiness : Bussiness<Observacion>
    {
        public ObservacionBussiness() : base("Observaciones", "Id", new ObservacionMapper())
        {
            this.columns = new List<string>() { "Observacion", "UserId", "CreatedAt", "TicketId" };
        }

        public List<Observacion> getObservacionsByTicket(long ticketId)
        {
            return select($"{idColumn}, {String.Join(" ,", columns)}", $" WHERE TicketId={ticketId}");
        }
    }
}
