using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Role
    {
        public enum ROLES
        {
            OPERATOR = 1,
            SUPERVISOR,
            ADMIN
        }

        public ROLES Id { get; set; }

        public string Name { get; set; }
    }
}
