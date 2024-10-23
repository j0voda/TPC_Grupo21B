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
    internal class UserMapper : IDBMapper<User>
    {
        public string getIdentifier(User obj)
        {
            return obj.Id.ToString();
        }

        public List<string> mapFromObject(User obj)
        {
            return new QueryValuesBuilder()
                .setStringValue(obj.Username)
                .setStringValue(obj.Password)
                .setStringValue(obj.Nombres)
                .setStringValue(obj.Apellidos)
                .setStringValue(obj.Email)
                .setDateValue(obj.CreatedAt)
                .setDateValue(obj.LastUpdatedAt)
                .setStringValue(obj.Documento)
                .setStringValue(obj.Sexo)
                .setIntValue(obj.RolId)
                .build();
        }

        public User mapToObject(SqlDataReader reader)
        {
            User user = new User();

            user.Id = reader.GetInt64(0);
            user.Username = reader.GetString(1);
            user.Password = reader.GetString(2);
            user.Nombres = reader.GetString(3);
            user.Apellidos = reader.GetString(4);
            user.Email = reader.GetString(5);
            user.CreatedAt = reader.GetDateTime(6);
            user.LastUpdatedAt = reader.GetDateTime(7);
            user.Documento = reader.GetInt64(8);
            user.Sexo = reader.GetString(9);
            user.RolId = reader.GetInt32(10);

            return user;
        }
    }
}
