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
                "Id",
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
    }
}
