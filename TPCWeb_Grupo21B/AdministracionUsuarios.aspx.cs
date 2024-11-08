using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCWeb_Grupo21B
{
    public partial class AdministracionUsuarios : System.Web.UI.Page
    {

        public List<User> users;
        private List<Role> roles;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthorizationManager.getInstance().hasPermission(AuthorizationManager.PERMISSIONS.USER_MANAGEMENT))
            {
                Response.Redirect("/Default");
            }

            var userBusiness = new UserBusiness();

            this.users = userBusiness.getAll();

            var roleBussiness = new RoleBussiness();

            this.roles = roleBussiness.getAll();

            this.sltRole.DataSource = roles.Select(r => new ListItem() { Text=r.Name, Value=((int)r.Id).ToString() }).ToList();

            this.sltRole.DataBind();
        }

        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            var mail = this.tbMail.Text.Trim();
            var document = this.tbDocument.Text.Trim();
            var gender = this.sltSex.Text.Trim();
            var role = this.roles[this.sltRole.SelectedIndex];
            
            Random random = new Random();
            
            var temporalPassword = random.Next(1000, 9999).ToString();

            var user = new User() { 
                Email = mail, 
                Documento = long.Parse(document), 
                Sexo = gender, 
                Rol = role, 
                Password=temporalPassword,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                //Estado = new UserState() { Id = UserState.USER_STATES.INACTIVE }
            };

            var userBussines = new UserBusiness();

            var savedUser = userBussines.saveOne(user);

            if (savedUser == -1)
            {
                throw new Exception("Error al guardar el usuario");
            }

            MailService.sendMail(
                mail, 
                "Usuario", 
                "Termina tu registro",
                $"Intenta iniciar sesión en la pantalla principal con esta contraseña temporal para terminar tu proceso de registro: {temporalPassword}. \n Por seguridad no compartas tu contraseña con nadie."
            );

        }
    }
}