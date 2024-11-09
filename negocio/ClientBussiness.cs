using acceso_datos;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ClientBussiness: Bussiness<Client>
    {

        public ClientBussiness(): base("Clientes", "Documento", new ClientMapper(), false) {
            this.columns = new List<string> { "Nombres", "Apellidos", "Email" };
        }

        override public int saveOne(Client item)
        {
            base.saveOne(item);

            return (int)item.Document;
        }
    }
}
