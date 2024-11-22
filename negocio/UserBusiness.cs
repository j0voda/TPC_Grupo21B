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
                "EstadoId"
            };
        }

        public override List<User> getAll()
        {
            return select($"t.{idColumn}, {String.Join(" ,", columns)}, r.Descripcion as RoleDescription, e.Descripcion as EstadoDescription",
                " INNER JOIN Roles r ON r.Id=t.RolId INNER JOIN Estados_Usuarios e ON e.Id = t.EstadoId WHERE NOT t.EstadoId = 3");
        }

        public User getOneByUserPass(string user, string pass)
        {
            List<User> result = base.select(
                $"t.{idColumn}, t.{String.Join(" ,t.", columns)}, r.Descripcion as RoleDescription, e.Descripcion as EstadoDescription", 
                $" INNER JOIN Roles r ON r.Id=t.RolId INNER JOIN Estados_Usuarios e ON e.Id = t.EstadoId WHERE (t.Username='{user}' OR t.Email='{user}') AND t.Password='{pass}' AND NOT t.EstadoId = 3");

            if (result.Count == 0)
            {
                return default;
            }

            return result[0];
        }

        public override void deleteOne(User item)
        {
            item.Estado = new UserState() { Id = (int)UserState.USER_STATES.DELETED };

            base.updateOne(item);
        }

        public override int saveOne(User item)
        {

            var existing = this.getOneByDocument(item);

            if (existing == null)
            {
                return base.saveOne(item);
            }

            if (existing.Estado.Id != (int)UserState.USER_STATES.DELETED) return -1;

            item.Id = existing.Id;

            base.updateOne(item);

            return ((int)item.Id);
        }

        private User getOneByDocument(User item)
        {
            List<User> res = select(
                $"t.{idColumn}, t.{String.Join(" ,t.", columns)}, r.Descripcion as RoleDescription, e.Descripcion as EstadoDescription", 
                $" INNER JOIN Roles r ON r.Id=t.RolId INNER JOIN Estados_Usuarios e ON e.Id = t.EstadoId WHERE Documento={item.Documento}");

            if (res.Count == 0)
            {
                return default;
            }

            return res[0];
        }

    }
}
