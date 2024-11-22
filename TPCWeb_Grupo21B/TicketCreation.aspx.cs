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
        private List<User> users;
        private Client selectedClient;
        public User user;
        public dominio.Ticket ticket;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            loadData();

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
            loadUsers();

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

        private void loadUsers()
        {
            var auth = AuthorizationManager.getInstance();
            var usersB = new UserBusiness();
            var uState = new UserState();
            users = usersB.getAll();

            this.ddUsuarios.DataSource = users.Where(u => u.Estado.Id == UserState.USER_STATES.ACTIVE).ToList();
            this.ddUsuarios.DataValueField = "Id";
            this.ddUsuarios.DataTextField = "Username";
            this.ddUsuarios.SelectedValue = auth.User.Id.ToString();
            this.ddUsuarios.DataBind();
            // Agregar el ítem de valor por defecto al inicio de la lista
            //ddUsuarios.Items.Insert(0, new ListItem("Sin asignar", "-1"));
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
            var auth = AuthorizationManager.getInstance();

            var doc = txtCliente.Text.Split(',')[0];

            if(validateAsunto() && validateDescription() && validateClient())
            {
                ticket = new dominio.Ticket();
                ticket.Asunto = txtAsunto.Text;
                ticket.ClientDocument = Int64.Parse(doc);
                ticket.UserId = auth.hasPermission(AuthorizationManager.PERMISSIONS.TICKET_ASSIGN) ? Convert.ToInt64(this.ddUsuarios.SelectedValue) : auth.User.Id;
            
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
                int result = tcktBus.saveOne(ticket);

                if (result < 0)
                {
                    return;
                }

                ObservacionBussiness observacionBussiness = new ObservacionBussiness();
                string obsText = "";
                var obs = new Observacion()
                {
                    Observation = obsText,
                    UserId = auth.User.Id,
                    TicketId = result
                };

                obs.Observation = obs.GenerateAutomaticObservation(4, "Abierto");
                observacionBussiness.saveOne(obs);
                
                if(ticket.UserId >= 0)
                {
                    string asign = "";
                    var obsAsign = new Observacion()
                    {
                        Observation = asign,
                        UserId = auth.User.Id,
                        TicketId = result
                    };

                    obsAsign.Observation = obsAsign.GenerateAutomaticObservation(4, "Asignado");
                    observacionBussiness.saveOne(obsAsign);

                    ticket.Id = result;
                    ticket.Estado.Id = 5;
                    tcktBus.updateOne(ticket);
                }

                Response.Redirect("/Default");
            }

            return;
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

        private bool validateAsunto()
        {
            bool valid = true;
            string asuntotxt = this.txtAsunto.Text.Trim();

            if (string.IsNullOrEmpty(asuntotxt))
            {
                this.txtAsunto.CssClass = addIsInvalidClass(this.txtAsunto.CssClass);
                this.asuntoError.Text = "Debe completar el campo.";
                valid = false;
            }
            else if (asuntotxt.Any(ch => !char.IsLetterOrDigit(ch) && ch != ' '))
            {
                this.txtAsunto.CssClass = addIsInvalidClass(this.txtAsunto.CssClass);
                this.asuntoError.Text = "Ingrese solo letras.";
                valid = false;
            }
            else
            {
                this.txtAsunto.CssClass = removeIsInvalidClass(this.txtAsunto.CssClass);
            }

            return valid;
        }

        private bool validateDescription()
        {
            bool valid = true;
            string desctxt = this.txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(desctxt))
            {
                this.txtDescripcion.CssClass = addIsInvalidClass(this.txtDescripcion.CssClass);
                this.descError.Text = "Debe completar el campo.";
                valid = false;
            }
            else if (desctxt.Any(ch => !char.IsLetterOrDigit(ch) && ch != ' '))
            {
                this.txtDescripcion.CssClass = addIsInvalidClass(this.txtDescripcion.CssClass);
                this.descError.Text = "Ingrese solo letras.";
                valid = false;
            }
            else
            {
                this.txtDescripcion.CssClass = removeIsInvalidClass(this.txtDescripcion.CssClass);
            }

            return valid;
        }

        private bool validateClient()
        {
            bool valid = true;
            string clientId = this.txtCliente.Text.Trim();

            if (string.IsNullOrEmpty(clientId))
            {
                this.txtCliente.CssClass = addIsInvalidClass(this.txtCliente.CssClass);
                this.clientError.Text = "Debe completar el campo.";
                valid = false;
            }
            else
            {
                this.clientError.CssClass = removeIsInvalidClass(this.txtCliente.CssClass);
            }

            return valid;
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
    }
}