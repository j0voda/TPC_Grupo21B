using acceso_datos;
using dominio;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace negocio
{
    internal class ClasificacionMapper : IDBMapper<Clasificacion>
    {
        public string getIdentifier(Clasificacion obj)
        {
            throw new NotImplementedException();
        }

        public List<string> mapFromObject(Clasificacion obj)
        {
            throw new NotImplementedException();
        }

        public Clasificacion mapToObject(SqlDataReader reader)
        {
            Clasificacion clas = new Clasificacion();

            clas.Id = reader.GetInt32(0);
            clas.Descripcion = reader.GetString(1);

            return clas;
        }
    }
}