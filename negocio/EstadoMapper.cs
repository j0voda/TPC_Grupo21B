using acceso_datos;
using dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace negocio
{
    internal class EstadoMapper : IDBMapper<Estado>
    {
        public string getIdentifier(Estado obj)
        {
            throw new NotImplementedException();
        }

        public List<string> mapFromObject(Estado obj)
        {
            throw new NotImplementedException();
        }

        public Estado mapToObject(SqlDataReader reader)
        {
            Estado state = new Estado();

            state.Id = reader.GetInt32(0);
            state.Descripcion = reader.GetString(1);

            return state;
        }
    }
}