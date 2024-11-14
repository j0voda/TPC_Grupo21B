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
        public List<User> users;
        public List<Estado> estados;
        public List<Clasificacion> clasificaciones;
        public List<Prioridad> prioridades;
        public List<Client> clientes;
        protected void Page_Load(object sender, EventArgs e)
        {
            var auth = AuthorizationManager.getInstance();

            if (!auth.isLogIn())
            {
                Response.Redirect("Login.aspx", true);
            }

            user = auth.User;
            
            if(!Page.IsPostBack)
            {

                loadData();

                TicketBusiness ticketBusiness = new TicketBusiness();
                if (user != null)
                {
                    tickets = auth.hasPermission(AuthorizationManager.PERMISSIONS.SEE_ALL_TICKETS) ? ticketBusiness.getAll() : ticketBusiness.getAllByUserId(user.Id);

                    if (tickets == null)
                    {
                        tickets = new List<Ticket>();
                    }

                    var userB = new UserBusiness();

                    users = userB.getAll();

                    rptTickets.DataSource = tickets;
                    rptTickets.DataBind();
                }
            }

        }

        private void loadData()
        {
            ClasificacionBussiness clasBusiness = new ClasificacionBussiness();
            PrioridadBussiness prioBusiness = new PrioridadBussiness();
            EstadoBussiness estBusiness = new EstadoBussiness();
            ClientBussiness clientBusiness = new ClientBussiness();

            clasificaciones = clasBusiness.getAll();
            prioridades = prioBusiness.getAll();
            estados = estBusiness.getAll();
            clientes = clientBusiness.getAll();

            this.ddlFltPrio.DataSource = prioridades;
            this.ddlFltPrio.DataValueField = "Id";
            this.ddlFltPrio.DataTextField = "Descripcion";
            this.ddlFltPrio.DataBind();
            // Agregar el ítem de valor por defecto al inicio de la lista
            ddlFltPrio.Items.Insert(0, new ListItem("Todos", "-1"));

            this.ddlFltClas.DataSource = clasificaciones;
            this.ddlFltClas.DataValueField = "Id";
            this.ddlFltClas.DataTextField = "Descripcion";
            this.ddlFltClas.DataBind();
            // Agregar el ítem de valor por defecto al inicio de la lista
            ddlFltClas.Items.Insert(0, new ListItem("Todos", "-1"));

            this.ddlFltEst.DataSource = estados;
            this.ddlFltEst.DataValueField = "Id";
            this.ddlFltEst.DataTextField = "Descripcion";
            this.ddlFltEst.DataBind();
            // Agregar el ítem de valor por defecto al inicio de la lista
            ddlFltEst.Items.Insert(0, new ListItem("Todos", "-1"));

            this.ddlFltCli.DataSource = clientes;
            this.ddlFltCli.DataValueField = "Document";
            this.ddlFltCli.DataTextField = "Name";
            this.ddlFltCli.DataBind();
            // Agregar el ítem de valor por defecto al inicio de la lista
            ddlFltCli.Items.Insert(0, new ListItem("Todos", "-1"));
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

        public string getUserName(long userId)
        {
            var userB = new UserBusiness();
            users = userB.getAll();

            var us = this.users.Find(u => u.Id == (long)Eval("UserId"));
            return $"{us.Apellidos}, {us.Nombres}";
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            TicketBusiness ticketBusiness = new TicketBusiness();
            var auth = AuthorizationManager.getInstance();

            int clasFilter = Convert.ToInt32(this.ddlFltClas.SelectedItem.Value);
            int estFilter = Convert.ToInt32(this.ddlFltEst.SelectedItem.Value);
            int prioFilter = Convert.ToInt32(this.ddlFltPrio.SelectedItem.Value);
            int idFilter = Convert.ToInt32(String.IsNullOrEmpty(this.txtFltId.Text) ? "-1" : this.txtFltId.Text);
            long idClient = Convert.ToInt64(this.ddlFltCli.SelectedItem.Value);

            tickets = auth.hasPermission(AuthorizationManager.PERMISSIONS.SEE_ALL_TICKETS) ? ticketBusiness.getAll() : ticketBusiness.getAllByUserId(user.Id);

            if(idFilter >= 0)
            {
                tickets.RemoveAll(t => t.Id != idFilter);
            }

            if (idClient >= 0)
            {
                tickets.RemoveAll(t => t.ClientDocument !=  idClient);
            }

            if (clasFilter >= 0)
            {
                tickets.RemoveAll(t => t.Clasificacion.Id != clasFilter);
            }

            if (estFilter >= 0)
            {
                tickets.RemoveAll(t => t.Estado.Id != estFilter);
            }

            if (prioFilter >= 0)
            {
                tickets.RemoveAll(t => t.Prioridad.Id != prioFilter);
            }

            rptTickets.DataSource = tickets;
            rptTickets.DataBind();
        }
    }
}