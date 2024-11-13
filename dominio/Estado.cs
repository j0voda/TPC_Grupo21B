using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Estado
    {
        public enum STATES_CODES { 
            OPEN = 1,
            ON_ANALISIS,
            CLOSE,
            REOPEN,
            ASSIGNED,
            SOLVED
        }

        public Int32 Id { get; set; }
        public string Descripcion { get; set; }
    }
}