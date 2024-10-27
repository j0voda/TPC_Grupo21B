using acceso_datos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class RoleBussiness : Bussiness<Role>
    {
        public RoleBussiness() : base("Roles", "Id", new RoleMapper())
        {
            this.columns = new List<string> {
                "Descripcion"
            };
        }
    }
}
