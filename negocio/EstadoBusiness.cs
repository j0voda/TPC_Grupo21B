using acceso_datos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class EstadoBussiness : Bussiness<Estado>
    {
        public EstadoBussiness() : base("Estados", "Id", new EstadoMapper())
        {
            this.columns = new List<string> {
                "Descripcion"
            };
        }

        public override List<Estado> getAll()
        {
            List<Estado> result = base.select(
                $"*");

            if (result.Count == 0)
            {
                return default;
            }

            return result;
        }
    }
}
