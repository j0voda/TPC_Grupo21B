using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace TPCWeb_Grupo21B.Screens
{
    public partial class Ticket : System.Web.UI.Page
    {
        public List<Clasificacion> clasificaciones;
        public List<Prioridad> prioridades;
        public List<User> users;
        public User user;
        public dominio.Ticket ticketM;
        public Client Client;
        public List<Observacion> observacions;
        protected void Page_Load(object sender, EventArgs e)
        {
            var auth = AuthorizationManager.getInstance();
            if (!auth.isLogIn())
            {
                Response.Redirect("Login.aspx", true);
            }
            user = auth.User;

            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["ticketId"]))
                {
                    string ticketId = Request.QueryString["ticketId"];
                    cargarTicket(ticketId);

                    ClasificacionBussiness clasBusiness = new ClasificacionBussiness();
                    PrioridadBussiness prioBusiness = new PrioridadBussiness();
                    UserBusiness userBusiness = new UserBusiness();
                    ClientBussiness clientBussiness = new ClientBussiness();

                    if (ticketM != null)
                    {
                        clasificaciones = clasBusiness.getAll();
                        prioridades = prioBusiness.getAll();
                        users = userBusiness.getAll();

                        this.prioSelect.DataSource = prioridades;
                        this.prioSelect.DataValueField = "Id";
                        this.prioSelect.DataTextField = "Descripcion";
                        this.prioSelect.SelectedValue = ticketM.Prioridad.Id.ToString();
                        this.prioSelect.DataBind();

                        this.clasSelect.DataSource = clasificaciones;
                        this.clasSelect.DataValueField = "Id";
                        this.clasSelect.DataTextField = "Descripcion";
                        this.clasSelect.SelectedValue = ticketM.Clasificacion.Id.ToString();
                        this.clasSelect.DataBind();

                        this.userSelect.DataSource = users;
                        this.userSelect.DataValueField = "Id";
                        this.userSelect.DataTextField = "Username";
                        this.userSelect.SelectedValue = ticketM.UserId.ToString();
                        this.userSelect.DataBind();

                        if (!auth.hasPermission(AuthorizationManager.PERMISSIONS.TICKET_ASSIGN))
                        {
                            this.userSelect.Enabled = false;
                        }

                        Client = clientBussiness.getOne(new Client() { Document = ticketM.ClientDocument });

                        var obsB = new ObservacionBussiness();

                        this.observacions = obsB.getObservacionsByTicket(ticketM.Id);
                    }
                }
            }

        }

        private void cargarTicket(string ticketId)
        {
            if (!string.IsNullOrEmpty(ticketId))
            {
                TicketBusiness ticketBusiness = new TicketBusiness();
                dominio.Ticket ticket = ticketBusiness.getOne(Convert.ToInt32(ticketId));

                if (ticket != null)
                {
                    Session["ticket"] = ticket;
                    ticketM = ticket;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            ticket.LastUpdatedAt = DateTime.Now;

            TicketBusiness tcktBus = new TicketBusiness();

            // Reviso si requiere un cambio de estado
            var originalTicket = tcktBus.getOne(Convert.ToInt32(ticket.Id));
            
            if (originalTicket.UserId != ticket.UserId)
            {
                ticket.Estado = new Estado
                {
                    Id = (int)Estado.STATES_CODES.ASSIGNED
                };
            }

            // Guardado en db
            tcktBus.updateOne(ticket);

            Response.Redirect("/Default");
        }

        protected void prioSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int prioIndex = Convert.ToInt32(prioSelect.SelectedValue);
            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            ticket.Prioridad.Id = prioIndex;
            Session["ticket"] = ticket;
        }

        protected void clasSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            int clasIndex = Convert.ToInt32(clasSelect.SelectedValue);
            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            ticket.Clasificacion.Id = clasIndex;
            Session["ticket"] = ticket;
        }

        protected void userSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            int userIndex = Convert.ToInt32(userSelect.SelectedValue);
            ticket.UserId = userIndex;
            Session["ticket"] = ticket;
        }
    
        public string getUserNameById(long id)
        {
            if (users == null)
            {
                var userBussiness = new UserBusiness();
                users = userBussiness.getAll();
            }

            var userObs = this.users.Find(u => u.Id == id);

            return $"{userObs.Apellidos}, {userObs.Nombres}";
        }

        protected void btnReAbrir_Click(object sender, EventArgs e)
        {
            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            ticket.Estado.Id = 4;

            // Guardado en db
            TicketBusiness tcktBus = new TicketBusiness();
            tcktBus.updateOne(ticket);

            Response.Redirect("/Default");
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            ticket.Estado.Id = 3;

            // Guardado en db
            TicketBusiness tcktBus = new TicketBusiness();
            tcktBus.updateOne(ticket);

            Response.Redirect("/Default");
        }
    }
}