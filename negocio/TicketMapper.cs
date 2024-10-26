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
    internal class TicketMapper : IDBMapper<Ticket>
    {
        public string getIdentifier(Ticket obj)
        {
            return obj.Id.ToString();
        }

        public List<string> mapFromObject(Ticket obj)
        {
            return new QueryValuesBuilder()
                //.setStringValue(obj.Username)
                //.setStringValue(obj.Password)
                //.setStringValue(obj.Nombres)
                //.setStringValue(obj.Apellidos)
                //.setStringValue(obj.Email)
                //.setDateValue(obj.CreatedAt)
                //.setDateValue(obj.LastUpdatedAt)
                //.setStringValue(obj.Documento)
                //.setStringValue(obj.Sexo)
                //.setIntValue(obj.RolId)
                .build();
        }

        public Ticket mapToObject(SqlDataReader reader)
        {
            Ticket ticket = new Ticket();
            ticket.Estado = new Estado();
            ticket.Clasificacion = new Clasificacion();
            ticket.Prioridad = new Prioridad();

            // TODO : Refactor de la clase Ticket para insertar los valores como corresponde
            ticket.Id = reader.GetInt64(0);
            ticket.Asunto = reader.GetString(1);
            ticket.Descripcion = reader.GetString(2);
            ticket.UserId = reader.GetInt64(3);
            ticket.ClientDocument = reader.GetInt64(4);
            ticket.Estado.Id = reader.GetInt32(5);
            ticket.Estado.Descripcion = reader.GetString(6);
            ticket.CreatedAt = reader.GetDateTime(7);
            ticket.LastUpdatedAt = reader.GetDateTime(8);
            ticket.Clasificacion.Id = reader.GetInt32(9);
            ticket.Clasificacion.Descripcion = reader.GetString(10);
            ticket.Prioridad.Id = reader.GetInt32(11);
            ticket.Prioridad.Descripcion = reader.GetString(12);

            return ticket;
        }
    }
}
