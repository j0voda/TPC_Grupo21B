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
    public partial class TicketCreation : System.Web.UI.Page
    {
        public List<Clasificacion> clasificaciones;
        public List<Prioridad> prioridades;
        private List<Client> clients;
        private Client selectedClient;
        public User user;
        public dominio.Ticket ticket;
        protected void Page_Load(object sender, EventArgs e)
        {
            var auth = AuthorizationManager.getInstance();
            if (!auth.isLogIn())
            {
                Response.Redirect("Login.aspx", true);
            }
            user = auth.User;

            loadClients();

            if (!IsPostBack)
            {
                ClasificacionBussiness clasBusiness = new ClasificacionBussiness();
                PrioridadBussiness prioBusiness = new PrioridadBussiness();

                if (user != null)
                {
                    clasificaciones = clasBusiness.getAll();
                    prioridades = prioBusiness.getAll();

                    this.prioSelect.DataSource = prioridades;
                    this.prioSelect.DataValueField = "Id";
                    this.prioSelect.DataTextField = "Descripcion";
                    this.prioSelect.DataBind();

                    this.clasSelect.DataSource = clasificaciones;
                    this.clasSelect.DataValueField = "Id";
                    this.clasSelect.DataTextField = "Descripcion";
                    this.clasSelect.DataBind();
                }
            }
        }

        private void loadClients()
        {
            var clientsB = new ClientBussiness();

            clients = clientsB.getAll();

            this.ddCliente.DataSource = clients
                .Where(c =>
                {
                    var q = this.txtCliente.Text;

                    return c.ToString().Contains(q);
                });
            this.ddCliente.DataBind();
        }
        protected void prioSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void clasSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtAsunto_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            loadClients();
        }

        protected void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            // TODO: Validators
            ticket = new dominio.Ticket();
            ticket.Asunto = txtAsunto.Text;
            ticket.ClientDocument = Int64.Parse(txtCliente.Text);
            ticket.UserId = user.Id;
            
            ticket.Estado = new Estado();
            ticket.Estado.Id = 1;

            ticket.CreatedAt = DateTime.Now;
            ticket.LastUpdatedAt = DateTime.Now;

            ticket.Clasificacion = new Clasificacion();
            ticket.Clasificacion.Id = Convert.ToInt32(this.clasSelect.SelectedItem.Value);

            ticket.Prioridad = new Prioridad();
            ticket.Prioridad.Id = Convert.ToInt32(this.prioSelect.SelectedItem.Value);

            ticket.Descripcion = txtDescripcion.Text;

            // Guardado en db
            TicketBusiness tcktBus = new TicketBusiness();
            int result = tcktBus.insert(ticket);

            if (result < 0)
            {
                return;
            }

            Response.Redirect("/Default");
        }

    }
}