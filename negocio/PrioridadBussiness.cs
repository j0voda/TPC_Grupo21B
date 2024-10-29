using acceso_datos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PrioridadBussiness : Bussiness<Prioridad>
    {
        public PrioridadBussiness() : base("Prioridades", "Id", new PrioridadMapper())
        {
            this.columns = new List<string> {
                "Id",
                "Descripcion",
                "Color"
            };
        }

        public override List<Prioridad> getAll()
        {
            List<Prioridad> result = base.select(
                $"*");

            if (result.Count == 0)
            {
                return default;
            }

            return result;
        }
    }
}
