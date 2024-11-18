using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        private AuthorizationManager auth;

        protected void Page_Load(object sender, EventArgs e)
        {
            auth = AuthorizationManager.getInstance();
            if (!auth.isLogIn())
            {
                Response.Redirect("Login.aspx", true);
            }
            user = auth.User;

            if (!string.IsNullOrEmpty(Request.QueryString["ticketId"]))
            {
                string ticketId = Request.QueryString["ticketId"];
                cargarTicket(ticketId);

                ClasificacionBussiness clasBusiness = new ClasificacionBussiness();
                PrioridadBussiness prioBusiness = new PrioridadBussiness();
                UserBusiness userBusiness = new UserBusiness();
                ClientBussiness clientBussiness = new ClientBussiness();

                Client = clientBussiness.getOne(new Client() { Document = ticketM.ClientDocument });

                cargarObservaciones();

                if (ticketM != null && !IsPostBack)
                {
                    clasificaciones = clasBusiness.getAll();
                    prioridades = prioBusiness.getAll();
                    users = userBusiness.getAll();

                    this.prioSelect.DataSource = prioridades;
                    this.prioSelect.DataValueField = "Id";
                    this.prioSelect.DataTextField = "Descripcion";

                    this.clasSelect.DataSource = clasificaciones;
                    this.clasSelect.DataValueField = "Id";
                    this.clasSelect.DataTextField = "Descripcion";

                    this.userSelect.DataSource = users;
                    this.userSelect.DataValueField = "Id";
                    this.userSelect.DataTextField = "Username";

                    this.prioSelect.SelectedValue = ticketM.Prioridad.Id.ToString();
                    this.clasSelect.SelectedValue = ticketM.Clasificacion.Id.ToString();
                    this.userSelect.SelectedValue = ticketM.UserId.ToString();

                    this.userSelect.DataBind();
                    this.clasSelect.DataBind();
                    this.prioSelect.DataBind();
                }

                updateInputs(ticketM);

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

        private void cargarObservaciones()
        {
            var obsB = new ObservacionBussiness();

            this.observacions = obsB.getObservacionsByTicket(ticketM.Id);
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

                var obsB = new ObservacionBussiness();
                string obsText = "";
                var obs = new Observacion()
                {
                    Observation = obsText,
                    UserId = auth.User.Id,
                    TicketId = ticketM.Id
                };

                obs.Observation = obs.GenerateAutomaticObservation(4, "Asignado");
                obsB.saveOne(obs);
            }

            int userIndex = Convert.ToInt32(userSelect.SelectedValue);
            int prioIndex = Convert.ToInt32(prioSelect.SelectedValue);
            int clasIndex = Convert.ToInt32(clasSelect.SelectedValue);

            // Genero observacion indicando cambios de clasificacion / prioridad
            if(prioIndex != ticket.Prioridad.Id)
            {
                var obsB = new ObservacionBussiness();
                string obsText = "";
                var obs = new Observacion()
                {
                    Observation = obsText,
                    UserId = auth.User.Id,
                    TicketId = ticketM.Id
                };

                obs.Observation = obs.GenerateAutomaticObservation(1, prioSelect.SelectedItem.Text);
                obsB.saveOne(obs);
            }

            if(clasIndex != ticket.Clasificacion.Id)
            {
                var obsB = new ObservacionBussiness();
                string obsText = "";
                var obs = new Observacion()
                {
                    Observation = obsText,
                    UserId = auth.User.Id,
                    TicketId = ticketM.Id,
                };

                obs.Observation = obs.GenerateAutomaticObservation(2, clasSelect.SelectedItem.Text);
                obsB.saveOne(obs);
            }

            if((userIndex != ticket.UserId) && auth.hasPermission(AuthorizationManager.PERMISSIONS.TICKET_ASSIGN))
            {
                var obsB = new ObservacionBussiness();
                string obsText = "";
                var obs = new Observacion()
                {
                    Observation = obsText,
                    UserId = auth.User.Id,
                    TicketId = ticketM.Id,
                };

                obs.Observation = obs.GenerateAutomaticObservation(3, userSelect.SelectedItem.Text);
                obsB.saveOne(obs);
            }

            ticket.Clasificacion.Id = clasIndex;
            ticket.Prioridad.Id = prioIndex;

            // Guardado en db
            tcktBus.updateOne(ticket);
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

        protected void btnSaveObs_Click(object sender, EventArgs e)
        {
            var obsText = this.tbObservation.Text.Trim();

            if (obsText.Length == 0) return;

            var obsB = new ObservacionBussiness();

            var obs = new Observacion()
            {
                Observation = obsText,
                UserId = auth.User.Id,
                TicketId = ticketM.Id
            };

            obsB.saveOne(obs);

            ticketM.Estado = new Estado() { Id = (int)Estado.STATES_CODES.ON_ANALISIS };

            var ticketB = new TicketBusiness();

            ticketB.updateOne(ticketM);

            tbObservation.Text = "";

            cargarObservaciones();

            cargarTicket(ticketM.Id.ToString());

            this.btnSaveObs.Enabled = false;

        }

        protected void btnReAbrir_Click(object sender, EventArgs e)
        {
            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            ticket.Estado.Id = 4;

            // Guardado en db
            TicketBusiness tcktBus = new TicketBusiness();
            tcktBus.updateOne(ticket);

            updateInputs(ticket);
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            ticket.Estado.Id = 3;

            // Guardado en db
            TicketBusiness tcktBus = new TicketBusiness();
            tcktBus.updateOne(ticket);

            updateInputs(ticket);
        }

        private void updateInputs(dominio.Ticket ticket)
        {

            var isClosed = ticket.Estado.Id.Equals((int)Estado.STATES_CODES.CLOSE) || ticketM.Estado.Id.Equals((int)Estado.STATES_CODES.SOLVED);
            var isOpen = !isClosed;

            this.userSelect.Enabled = isOpen && auth.hasPermission(AuthorizationManager.PERMISSIONS.TICKET_ASSIGN);
            this.prioSelect.Enabled = isOpen;
            this.clasSelect.Enabled = isOpen;
            this.btnGuardar.Enabled = isOpen;
            this.tbObservation.Enabled = isOpen;
            this.btnSaveObs.Enabled = isOpen && this.tbObservation.Text.Length > 0;
        }
    }
}