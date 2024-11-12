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
            loadData();

            if (IsPostBack) return;

            var auth = AuthorizationManager.getInstance();
            if (!auth.isLogIn())
            {
                Response.Redirect("Login.aspx", true);
            }
            user = auth.User;
        }

        private void loadData()
        {
            loadClients();

            ClasificacionBussiness clasBusiness = new ClasificacionBussiness();
            PrioridadBussiness prioBusiness = new PrioridadBussiness();

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

            if (ddCliente.Items.FindByText(txtCliente.Text) == null)
            {
                this.btnCrear.Enabled = false;
            } else
            {
                this.btnCrear.Enabled = true;
            }
        }

        protected void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {

            var doc = txtCliente.Text.Split(',')[0];

            // TODO: Validators
            ticket = new dominio.Ticket();
            ticket.Asunto = txtAsunto.Text;
            ticket.ClientDocument = Int64.Parse(doc);
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

        protected void btnNewClasification_Click(object sender, EventArgs e)
        {
            this.txtClasDesc.Text = "";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallOpenModal", "ClickOpenModalClas()", true);
        }

        protected void btnSaveClasif_Click(object sender, EventArgs e)
        {
            ClasificacionBussiness clasBusiness = new ClasificacionBussiness();
            Clasificacion clas = new Clasificacion();
            clas.Descripcion = this.txtClasDesc.Text;

            clasBusiness.saveOne(clas);

            loadData();
        }

        protected void btnSavePrio_Click(object sender, EventArgs e)
        {
            PrioridadBussiness prioBusiness = new PrioridadBussiness();
            Prioridad prio = new Prioridad();
            prio.Descripcion = this.txtPrioDesc.Text;
            prio.Color = this.txtPrioColor.Text;

            prioBusiness.saveOne(prio);

            loadData();
        }

        protected void btnNewPriority_Click(object sender, EventArgs e)
        {
            this.txtPrioDesc.Text = "";
            this.txtPrioColor.Text = "";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallOpenModal", "ClickOpenModalPrio()", true);
        }
    }
}