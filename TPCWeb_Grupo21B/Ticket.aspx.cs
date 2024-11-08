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

                        this.txtAsunto.Text = ticketM.Asunto;
                        this.txtCliente.Text = ticketM.ClientDocument.ToString();
                        this.txtDescripcion.Text = ticketM.Descripcion;
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

            // Guardado en db
            TicketBusiness tcktBus = new TicketBusiness();
            tcktBus.updateOne(ticket);

            Response.Redirect("/Default");
        }

        protected void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            string txtdesc = txtDescripcion.Text;
            if (string.IsNullOrEmpty(txtdesc))
            {
                return;
                // TODO: label de error
            }

            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            ticket.Descripcion = txtdesc;
            Session["ticket"] = ticket;
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

        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            string txtclient = txtCliente.Text;
            if (string.IsNullOrEmpty(txtclient))
            {
                return;
                // TODO: label de error
            }

            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            ticket.ClientDocument = Convert.ToInt64(txtclient);
            Session["ticket"] = ticket;
        }

        protected void txtAsunto_TextChanged(object sender, EventArgs e)
        {
            string txtasunto = txtAsunto.Text;
            if (string.IsNullOrEmpty(txtasunto))
            {
                return;
                // TODO: label de error
            }

            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            ticket.Asunto = txtasunto;
            Session["ticket"] = ticket;
        }

        protected void userSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            dominio.Ticket ticket = Session["ticket"] as dominio.Ticket;
            int userIndex = Convert.ToInt32(userSelect.SelectedValue);
            ticket.UserId = userIndex;
            Session["ticket"] = ticket;
        }
    }
}