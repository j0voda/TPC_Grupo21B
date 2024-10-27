using acceso_datos;
using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    internal class RoleMapper : IDBMapper<Role>
    {
        public string getIdentifier(Role obj)
        {
            throw new NotImplementedException();
        }

        public List<string> mapFromObject(Role obj)
        {
            throw new NotImplementedException();
        }

        public Role mapToObject(SqlDataReader reader)
        {
            Role role = new Role();

            role.Id = (Role.ROLES)reader.GetInt32(0);
            role.Name = reader.GetString(1);

            return role;
        }
    }
}
