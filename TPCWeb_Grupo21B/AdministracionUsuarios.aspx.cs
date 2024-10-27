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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthorizationManager.getInstance().hasPermission(AuthorizationManager.PERMISSIONS.USER_MANAGEMENT))
            {
                Response.Redirect("/Default");
            }

            var userBusiness = new UserBusiness();

            this.users = userBusiness.getAll();
        }
    }
}