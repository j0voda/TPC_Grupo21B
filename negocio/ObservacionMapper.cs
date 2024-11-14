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
    internal class ObservacionMapper : IDBMapper<Observacion>
    {
        public string getIdentifier(Observacion obj)
        {
            return obj.Id.ToString();
        }

        public List<string> mapFromObject(Observacion obj)
        {
            throw new NotImplementedException();
        }

        public Observacion mapToObject(SqlDataReader reader)
        {
            var obs = new Observacion();

            obs.Id = reader.GetInt32(0);
            obs.Observation = reader.GetString(1);
            obs.UserId = reader.GetInt64(2);
            obs.CreatedAt = reader.GetDateTime(3);
            obs.TicketId = reader.GetInt64(4);

            return obs;
        }
    }
}
