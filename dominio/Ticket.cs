using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public int UserId { get; set; }
        public string EstadoId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ClasificacionId { get; set; }
        public int PrioridadId { get; set; }
        public Event[] Eventos { get; set; }
    }
}
