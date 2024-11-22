using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCWeb_Grupo21B.Screens
{
    public partial class Register : System.Web.UI.Page
    {

        public User user;
        public bool usernameInUse = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthorizationManager.getInstance().isLogIn())
            {
                Response.Redirect("Login.aspx", true);
            }

            user = AuthorizationManager.getInstance().User;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var username = this.tbUsername.Text.Trim();
            var name = this.tbName.Text.Trim();
            var surname = this.tbSurname.Text.Trim();
            var password = this.tbPassword.Text.Trim();

            if (
                !checkValidation(this.tbUsername) || 
                !checkValidation(this.tbName) || 
                !checkValidation(this.tbSurname) ||
                !checkValidation(this.tbPassword)
                )
            {
                return;
            }

            var userBussiness = new UserBusiness();

            user = AuthorizationManager.getInstance().User;

            user.Username = username;
            user.Nombres = name;
            user.Apellidos = surname;
            user.Password = password;
            user.Estado = new UserState() { Id = (int)UserState.USER_STATES.ACTIVE };

            userBussiness.updateOne(user);

            MailService.sendMail(
                user.Email,
                user.Nombres + " " + user.Apellidos,
                "Registro completado",
                $"Bienvenido {user.Nombres + " " + user.Apellidos} su registro fue completado con exito. Ya puede volver a entrar y ver las opciones de su rol."
            );

            AuthorizationManager.getInstance().logOut();

            Response.Redirect("Login.aspx", true);
        }

        private bool checkValidation(TextBox tb)
        {
            var value = tb.Text.Trim();

            if (value.Length == 0)
            {
                tb.CssClass = addIsInvalidClass(tb.CssClass);
            }
            else
            {
                tb.CssClass = removeIsInvalidClass(tb.CssClass);
            }
            return value.Length > 0;
        }

        private string addIsInvalidClass(string classes)
        {
            if (!classes.Contains("is-valid")) return classes + " is-invalid";

            return classes;
        }

        private string removeIsInvalidClass(string classes)
        {
            return classes.Replace("is-invalid", "");
        }

        protected void tbUsername_TextChanged(object sender, EventArgs e)
        {
            var userB = new UserBusiness();
            this.usernameInUse = userB.userNameExists(tbUsername.Text.Trim());
            
            if (this.usernameInUse)
            {
                tbUsername.CssClass = addIsInvalidClass(tbUsername.CssClass);
            }
            else
            {
                tbUsername.CssClass = removeIsInvalidClass(tbUsername.CssClass);
            }

        }
    }
}