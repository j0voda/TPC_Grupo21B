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
        public Int64 Id { get; set; }
        public string Asunto { get; set; }
        public string Descripcion { get; set; }
        public Int64 UserId { get; set; }
        public Int64 ClientDocument { get; set; }
        public Estado Estado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Clasificacion Clasificacion { get; set; }
        public Prioridad Prioridad { get; set; }
    }
}
