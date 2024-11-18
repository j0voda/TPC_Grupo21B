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
            return new QueryValuesBuilder()
                .setStringValue(obj.Descripcion)
                .setStringValue(obj.Color)
                .setIntValue(obj.TimeToSolve)
                .build();
        }

        public Prioridad mapToObject(SqlDataReader reader)
        {
            Prioridad prio = new Prioridad();

            prio.Id = reader.GetInt32(0);
            prio.Descripcion = reader.GetString(1);
            prio.Color = reader.GetString(2);
            prio.TimeToSolve = reader.GetInt16(3);

            return prio;
        }
    }
}