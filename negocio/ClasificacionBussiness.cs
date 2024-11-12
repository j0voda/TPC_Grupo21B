using acceso_datos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ClasificacionBussiness : Bussiness<Clasificacion>
    {
        public ClasificacionBussiness() : base("Clasificaciones", "Id", new ClasificacionMapper())
        {
            this.columns = new List<string> {
                "Descripcion"
            };
        }

        public override List<Clasificacion> getAll()
        {
            List<Clasificacion> result = base.select(
                $"*");

            if (result.Count == 0)
            {
                return default;
            }

            return result;
        }

        public override int saveOne(Clasificacion item)
        {
            List<string> values = this.mapper.mapFromObject(item);

            int id = insert(String.Join(" ,", values));

            return id;
        }

        public override int insert(string values)
        {
            int id = -1;
            sqlConexion.Open();

            string query = String.Format("INSERT INTO {0} ({1}) VALUES ({2});SELECT SCOPE_IDENTITY();", tableName, String.Join(" ,", columns), values);

            reader = this.executeCommand(query);
            if (reader.Read())
            {
                id = Convert.ToInt32(reader[0]);
            }
            sqlConexion.Close();

            return id;
        }
    }
}
