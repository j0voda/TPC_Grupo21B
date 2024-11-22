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
                "Descripcion",
                "UserId",
                "ClientDocument",
                "EstadoId",
                "CreatedAt",
                "LastUpdatedAt",
                "ClasificacionId",
                "PrioridadId",
            };
        }

        public override List<Ticket> getAll()
        {
            List<Ticket> result = base.select(
                $"t.Id, t.Asunto, t.Descripcion, t.UserId, t.ClientDocument, e.Id as EstadoId, e.Descripcion as EstadoDesc, " +
                $"t.CreatedAt, t.LastUpdatedAt, c.Id as ClasificacionId, c.Descripcion as ClasificacionDesc, p.Id as PrioridadId, p.Descripcion as PrioridadDesc, p.Color as PrioridadColor, p.TimeToSolve as TimeToSolve", 
                $"INNER JOIN Estados e on t.EstadoId = e.Id " +
                $"INNER JOIN Clasificaciones c on t.ClasificacionId = c.Id " +
                $"INNER JOIN Prioridades p on t.PrioridadId = p.Id ORDER BY CreatedAt DESC");

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
                $"t.CreatedAt, t.LastUpdatedAt, c.Id as ClasificacionId, c.Descripcion as ClasificacionDesc, p.Id as PrioridadId, p.Descripcion as PrioridadDesc, p.Color as PrioridadColor, p.TimeToSolve as TimeToSolve",
                $"INNER JOIN Estados e on t.EstadoId = e.Id " +
                $"INNER JOIN Clasificaciones c on t.ClasificacionId = c.Id " +
                $"INNER JOIN Prioridades p on t.PrioridadId = p.Id " +
                $"WHERE t.UserId = {userId} ORDER BY CreatedAt DESC");

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
                $"t.Id, t.Asunto, t.Descripcion, t.UserId, t.ClientDocument, e.Id as EstadoId, e.Descripcion as EstadoDesc, " +
                $"t.CreatedAt, t.LastUpdatedAt, c.Id as ClasificacionId, c.Descripcion as ClasificacionDesc, p.Id as PrioridadId, p.Descripcion as PrioridadDesc, p.Color as PrioridadColor, p.TimeToSolve as TimeToSolve",
                $"INNER JOIN Estados e on t.EstadoId = e.Id " +
                $"INNER JOIN Clasificaciones c on t.ClasificacionId = c.Id " +
                $"INNER JOIN Prioridades p on t.PrioridadId = p.Id " +
                $"WHERE t.Id = {ticketId}");

            if (result.Count == 0)
            {
                return default;
            }

            return result[0];
        }
        
    }
}
