using acceso_datos;
using dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace negocio
{
    internal class PrioridadMapper : IDBMapper<Prioridad>
    {
        public string getIdentifier(Prioridad obj)
        {
            throw new NotImplementedException();
        }

        public List<string> mapFromObject(Prioridad obj)
        {
            throw new NotImplementedException();
        }

        public Prioridad mapToObject(SqlDataReader reader)
        {
            Prioridad prio = new Prioridad();

            prio.Id = reader.GetInt32(0);
            prio.Descripcion = reader.GetString(1);
            prio.Color = reader.GetString(2);

            return prio;
        }
    }
}