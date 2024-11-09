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
    internal class ClientMapper : IDBMapper<Client>
    {
        public string getIdentifier(Client obj)
        {
            return obj.Document.ToString();
        }

        public List<string> mapFromObject(Client obj)
        {
            return new QueryValuesBuilder()
                .setIntValue(obj.Document)
                .setStringValue(obj.Name)
                .setStringValue(obj.Surname)
                .setStringValue(obj.Email)
                .build();
        }

        public Client mapToObject(SqlDataReader reader)
        {
            var result = new Client();

            result.Document = reader.GetInt64(0);
            result.Name = reader.GetString(1);
            result.Surname = reader.GetString(2);
            result.Email = reader.GetString(3);

            return result;
        }
    }
}
