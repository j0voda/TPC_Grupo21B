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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void username_TextChanged(object sender, EventArgs e)
        {
            string user = this.username.Text;

            if (user == null || user.Length == 0)
            {
                updateErrors("Debe ingresar un usuario.");
            }
            else
            {
                updateErrors();
            }
        }

        protected void password_TextChanged(object sender, EventArgs e)
        {
            string pass = this.password.Text;

            if (pass == null || pass.Length == 0)
            {
                updateErrors("Debe ingresar una contraseña.");
            }
            else
            {
                updateErrors();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string user = this.username.Text;
            string pass = this.password.Text;

            if (pass == null || pass.Length == 0 || user == null || user.Length == 0)
            {
                updateErrors("Debe completar los campos.");
            }
            else
            {
                updateErrors();
            }

            UserBusiness userBusiness = new UserBusiness();

            User u = userBusiness.getOneByUserPass(user, pass);

            if (u == null)
            {
                updateErrors("Credenciales incorrectas.");
                return;
            }

            Session.Add("usuario", u.Id);

            Response.Redirect("Default.aspx");
        }

        private void updateErrors(string error = null)
        {
            if (error != null)
            {
                this.username.CssClass = addIsInvalidClass(this.username.CssClass);
                this.password.CssClass = addIsInvalidClass(this.password.CssClass);
                this.lblError.Text = error;
            }
            else
            {
                this.username.CssClass = addIsInvalidClass(this.username.CssClass);
                this.password.CssClass = removeIsInvalidClass(this.password.CssClass);
            }
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
    }
}