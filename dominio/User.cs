using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dominio
{
    public class User
    {

        public Int64 Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public Int64 Documento { get; set; }
        public string Sexo { get; set; }
        public Role Rol { get; set; }
        public UserState Estado { get; set; }
    }
}
