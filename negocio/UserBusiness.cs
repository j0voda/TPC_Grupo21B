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
                "LastUpdatedAt", 
                "Documento", 
                "Sexo", 
                "RolId",
                //"EstadoId"
            };
        }

        public override List<User> getAll()
        {
            return select($"t.{idColumn}, {String.Join(" ,", columns)}, r.Descripcion as RoleDescription", " INNER JOIN Roles r ON r.Id=t.RolId");
        }

        public User getOneByUserPass(string user, string pass)
        {
            List<User> result = base.select(
                $"t.{idColumn}, t.{String.Join(" ,t.", columns)}, r.Descripcion as RoleDescription", $" INNER JOIN Roles r ON r.Id=t.RolId WHERE (t.Username='{user}' OR t.Email='{user}') AND t.Password='{pass}'");

            if (result.Count == 0)
            {
                return default;
            }

            return result[0];
        }
    
    }
}
