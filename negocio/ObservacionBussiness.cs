using acceso_datos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ObservacionBussiness : Bussiness<Observacion>
    {
        public ObservacionBussiness() : base("Observaciones", "Id", new ObservacionMapper())
        {
            this.columns = new List<string>() { "Observacion", "UserId", "CreatedAt", "TicketId" };
        }

        public override int saveOne(Observacion item)
        {
            List<string> values = this.mapper.mapFromObject(item);

            int id = insert(String.Join(" ,", values));

            return id;
        }

        override public int insert(string values)
        {
            int id = -1;
            sqlConexion.Open();


            var cols = new List<string>(columns);
            cols.RemoveAt(2);

            var insertColumns = String.Join(",", cols);

            string query = String.Format("INSERT INTO {0} ({1}) VALUES ({2});SELECT SCOPE_IDENTITY();", tableName, insertColumns, values);
            reader = this.executeCommand(query);

            if (reader.Read())
            {
                id = Convert.ToInt32(reader[0]);
            }
            sqlConexion.Close();

            return id;
        }

        public List<Observacion> getObservacionsByTicket(long ticketId)
        {
            return select($"{idColumn}, {String.Join(" ,", columns)}", $" WHERE TicketId={ticketId}");
        }
    }
}
