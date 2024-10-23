using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using acceso_datos;
using dominio;

namespace negocio
{
    public class UserBusiness : Bussiness<User>
    {

        public UserBusiness() : base("Usuarios", "Id", new UserMapper())
        {
            this.columns = new List<string> { 
                "Username", 
                "Password", 
                "Nombres", 
                "Apellidos", 
                "Email", 
                "CreatedAt", 
                "LastUpdateAt", 
                "Documento", 
                "Sexo", 
                "RolId" 
            };
        }
    }
}
