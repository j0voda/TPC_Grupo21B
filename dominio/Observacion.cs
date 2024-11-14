using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Observacion
    {
        public int Id {  get; set; }
        public string Observation {  get; set; }
        public DateTime CreatedAt { get; set; }
        public long UserId { get; set; }
        public long TicketId { get; set; }
    }
}
