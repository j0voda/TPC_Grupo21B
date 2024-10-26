﻿using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPCWeb_Grupo21B
{
    public partial class _Default : Page
    {
        public User user;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!AuthorizationManager.getInstance().isLogIn())
            {
                Response.Redirect("Login.aspx", true);
            }

            user = AuthorizationManager.getInstance().User;
        }
    }
}