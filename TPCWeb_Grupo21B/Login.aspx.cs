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
            
        }

        protected void password_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string user = this.username.Text;
            string pass = this.password.Text;

            if (validateUsername() && validatePassword())
            {
                User u = AuthorizationManager.getInstance().logIn(user, pass);

                if (u == null)
                {
                    updateErrors("Credenciales incorrectas.");
                    return;    
                }

                if (u.Username == null || u.Username.Length == 0)
                {
                    Response.Redirect("Register.aspx", true);
                }

                Response.Redirect("Default.aspx");
            }
            else
            {
                return;
            }
        }

        private bool validateUsername()
        {
            bool valid = true;
            string user = this.username.Text;

            if (string.IsNullOrEmpty(user))
            {
                this.username.CssClass = addIsInvalidClass(this.username.CssClass);
                this.lblUerror.Text = "Debe completar el campo.";
                valid = false;
            }
            else if (user.Length < 5) 
            {
                this.username.CssClass = addIsInvalidClass(this.username.CssClass);
                this.lblUerror.Text = "Ingrese mínimo 5 caracteres.";
                valid = false;
            }
            else
            {
                this.username.CssClass = removeIsInvalidClass(this.username.CssClass);
            }

            return valid;
        }

        private bool validatePassword()
        {
            bool valid = true;
            string pass = this.password.Text;

            if (string.IsNullOrEmpty(pass))
            {
                this.password.CssClass = addIsInvalidClass(this.password.CssClass);
                this.lblPerror.Text = "Debe completar el campo.";
                valid = false;
            }
            else if(pass.Length < 5)
            {
                this.password.CssClass = addIsInvalidClass(this.password.CssClass);
                this.lblPerror.Text = "Ingrese mínimo 5 caracteres.";
                valid = false;
            }
            else
            {
                this.password.CssClass = removeIsInvalidClass(this.password.CssClass);
            }

            return valid;
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
                this.username.CssClass = removeIsInvalidClass(this.username.CssClass);
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