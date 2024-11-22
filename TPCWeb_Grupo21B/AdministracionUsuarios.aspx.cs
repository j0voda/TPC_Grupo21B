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
        public User selectedUser;
        public bool openModal = false;
        
        private List<Role> roles;

        private static string SELECTED_USER_KEY = "SelectedUser";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthorizationManager.getInstance().hasPermission(AuthorizationManager.PERMISSIONS.USER_MANAGEMENT))
            {
                Response.Redirect("/Default");
            }

            selectedUser = Session[SELECTED_USER_KEY] != null ? (User)Session[SELECTED_USER_KEY] : null;

            var userBusiness = new UserBusiness();

            this.users = userBusiness.getAll();

            this.rpUsers.DataSource = users;
            this.rpUsers.DataBind();

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
            
            var temporalPassword = random.Next(10000, 99999).ToString();

            var user = new User() { 
                Email = mail, 
                Documento = long.Parse(document), 
                Sexo = gender, 
                Rol = role, 
                Password=temporalPassword,
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                Estado = new UserState() { Id = (int)UserState.USER_STATES.INACTIVE }
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

            this.users = new UserBusiness().getAll();

            this.rpUsers.DataSource = users;
            this.rpUsers.DataBind();
        }

        protected void rpUsers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            long id = long.Parse(e.CommandArgument.ToString());

            this.selectedUser = users.Find((u) => u.Id.Equals(id));

            this.tbDocument.Text = selectedUser.Documento.ToString();
            this.tbDocument.Enabled = false;

            this.tbMail.Text = selectedUser.Email;
            this.sltSex.SelectedValue = selectedUser.Sexo;

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallOpenModal", "ClickOpenModal()", true);
            Session[SELECTED_USER_KEY] = selectedUser;
            this.openModal = true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var userB = new UserBusiness();

            userB.deleteOne(selectedUser);

            this.users = new UserBusiness().getAll();

            this.rpUsers.DataSource = users;
            this.rpUsers.DataBind();
        }

        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            this.selectedUser = null;

            this.tbDocument.Enabled = true;
            this.tbDocument.Text = "";
            this.tbMail.Text = "";
            this.sltSex.SelectedIndex = 0;

            Session[SELECTED_USER_KEY] = null;
            this.openModal = true;
        }
    }
}