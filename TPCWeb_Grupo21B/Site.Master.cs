using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCWeb_Grupo21B
{
    public partial class SiteMaster : MasterPage
    {

        private List<string> unAuthPaths = new List<string> { "Login", "Register", "Login.aspx", "Register.aspx" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (unAuthPaths.All((path) => !Request.Path.EndsWith(path)) && !AuthorizationManager.getInstance().isLogIn())
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            AuthorizationManager.getInstance().logOut();
            Response.Redirect("Login.aspx");
        }
    }
}