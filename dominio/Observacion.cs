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

        public string GenerateAutomaticObservation(int type, string desc)
        {
            string obs = "Actualización: Se cambió ";

            switch (type)
            {
                case 1:
                    obs += "Prioridad a -> ";
                    break;
                case 2:
                    obs += "Clasificación a -> ";
                    break;
                case 3:
                    obs += "Usuario asignado a -> ";
                    break;
                case 4:
                    obs += "Estado a -> ";
                    break;
                default:
                    break;
            }

            obs += desc;
            return obs;
        }
    }
}
