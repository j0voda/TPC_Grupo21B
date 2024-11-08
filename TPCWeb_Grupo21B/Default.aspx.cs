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
    public partial class _Default : Page
    {
        public User user;
        public List<Ticket> tickets;
        protected void Page_Load(object sender, EventArgs e)
        {

            var auth = AuthorizationManager.getInstance();

            if (!auth.isLogIn())
            {
                Response.Redirect("Login.aspx", true);
            }

            user = auth.User;

            TicketBusiness ticketBusiness = new TicketBusiness();
            if (user != null)
            {
                tickets = auth.hasPermission(AuthorizationManager.PERMISSIONS.SEE_ALL_TICKETS) ? ticketBusiness.getAll() : ticketBusiness.getAllByUserId(user.Id);

                if (tickets == null)
                {
                    tickets = new List<Ticket>();
                }

                rptTickets.DataSource = tickets;
                rptTickets.DataBind();
            }
        }

        protected void btnVer_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string ticketId = btn.CommandArgument.ToString();
            ticketId = ticketId.Trim();
        }

        protected void btnVerClick_Command(object sender, CommandEventArgs e)
        {
            Button btn = (Button)sender;
            string ticketId = btn.CommandArgument.ToString();

            Response.Redirect("/Ticket?ticketId=" + ticketId);
        }
    }
}