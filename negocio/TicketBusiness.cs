using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using acceso_datos;
using dominio;

namespace negocio
{
    public class TicketBusiness : Bussiness<Ticket>
    {
        public TicketBusiness() : base("Tickets", "Id", new TicketMapper())
        {
            this.columns = new List<string> {
                "Asunto",
                "UserId",
                "ClientDocument",
                "EstadoId",
                "CreatedAt",
                "LastUpdatedAt",
                "ClasificacionId",
                "PrioridadId",
                "Descripcion"
            };
        }

        public int insert(Ticket ticket)
        {
            int id = -1;
            sqlConexion.Open();

            string query = String.Format("INSERT INTO {0} ({1}) VALUES ({2});SELECT SCOPE_IDENTITY();", tableName, String.Join(" ,", columns), $"'{ticket.Asunto}', " +
                $"{ticket.UserId}, {ticket.ClientDocument}, {ticket.Estado.Id}, '{ticket.CreatedAt}', '{ticket.LastUpdatedAt}', {ticket.Clasificacion.Id}, " +
                $"{ticket.Prioridad.Id}, '{ticket.Descripcion}'");
            reader = this.executeCommand(query);

            if (reader.Read())
            {
                id = Convert.ToInt32(reader[0]);
            }

            sqlConexion.Close();

            return id;
        }

        public override List<Ticket> getAll()
        {
            List<Ticket> result = base.select(
                $"t.Id, t.Asunto, t.Descripcion, t.UserId, t.ClientDocument, e.Id as EstadoId, e.Descripcion as EstadoDesc, " +
                $"t.CreatedAt, t.LastUpdatedAt, c.Id as ClasificacionId, c.Descripcion as ClasificacionDesc, p.Id as PrioridadId, p.Descripcion as PrioridadDesc, p.Color as PrioridadColor", 
                $"INNER JOIN Estados e on t.EstadoId = e.Id " +
                $"INNER JOIN Clasificaciones c on t.ClasificacionId = c.Id " +
                $"INNER JOIN Prioridades p on t.PrioridadId = p.Id");

            if (result.Count == 0)
            {
                return default;
            }

            return result;
        }

        public List<Ticket> getAllByUserId(Int64 userId)
        {
            List<Ticket> result = base.select(
                $"t.Id, t.Asunto, t.Descripcion, t.UserId, t.ClientDocument, e.Id as EstadoId, e.Descripcion as EstadoDesc, " +
                $"t.CreatedAt, t.LastUpdatedAt, c.Id as ClasificacionId, c.Descripcion as ClasificacionDesc, p.Id as PrioridadId, p.Descripcion as PrioridadDesc, p.Color as PrioridadColor",
                $"INNER JOIN Estados e on t.EstadoId = e.Id " +
                $"INNER JOIN Clasificaciones c on t.ClasificacionId = c.Id " +
                $"INNER JOIN Prioridades p on t.PrioridadId = p.Id " +
                $"WHERE t.UserId = {userId}");

            if (result.Count == 0)
            {
                return default;
            }

            return result;
        }

        public List<Ticket> getAllByEstadoId(int estadoId)
        {
            List<Ticket> result = base.select(
                $"t.{idColumn}, t.{String.Join(" ,t.", columns)}", $"INNER JOIN Estados e on t.EstadoId = e.Id " +
                $"INNER JOIN Clasificaciones c on t.ClasificacionId = c.Id " +
                $"INNER JOIN Prioridades p on t.PrioridadId = p.Id" +
                $"WHERE t.EstadoId = {estadoId}");

            if (result.Count == 0)
            {
                return default;
            }

            return result;
        }

        public List<Ticket> getAllByClasificacionId(int clasId)
        {
            List<Ticket> result = base.select(
                $"t.{idColumn}, t.{String.Join(" ,t.", columns)}", $"INNER JOIN Estados e on t.EstadoId = e.Id " +
                $"INNER JOIN Clasificaciones c on t.ClasificacionId = c.Id " +
                $"INNER JOIN Prioridades p on t.PrioridadId = p.Id" +
                $"WHERE t.ClasificacionId = {clasId}");

            if (result.Count == 0)
            {
                return default;
            }

            return result;
        }

        public Ticket getOne(int ticketId)
        {
            List<Ticket> result = base.select(
                $"t.{idColumn}, t.{String.Join(" ,t.", columns)}", $"INNER JOIN Estados e on t.EstadoId = e.Id " +
                $"INNER JOIN Clasificaciones c on t.ClasificacionId = c.Id " +
                $"INNER JOIN Prioridades p on t.PrioridadId = p.Id" +
                $"WHERE t.Id = {ticketId}");

            if (result.Count == 0)
            {
                return default;
            }

            return result[0];
        }
    }
}
