using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class UserState
    {

        public enum USER_STATES
        {
            ACTIVE = 1,
            INACTIVE,
            DELETED
        }

        public int Id { get; set; }

        public string Name { get; set; }

    }
}
