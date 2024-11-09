using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TPCWeb_Grupo21B
{
    public partial class AdministracionClientes : System.Web.UI.Page
    {

        public List<Client> Clients { get; set; }
        public Client SelectedClient { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            loadData();

            if (IsPostBack) return;

            if (!AuthorizationManager.getInstance().hasPermission(AuthorizationManager.PERMISSIONS.CLIENT_MANAGENT))
            {
                Response.Redirect("/Default");
            }
        }

        private void loadData()
        {
            var clientB = new ClientBussiness();

            Clients = clientB.getAll();

            this.rpClients.DataSource = Clients;
            this.rpClients.DataBind();
        }

        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            var doc = this.tbDocument.Text;
            var mail = this.tbMail.Text;
            var name = this.tbName.Text;
            var surname = this.tbSurname.Text;
        
            if (!UtilValidators.isNotNullOrEmpty(doc, mail, name, surname))
            {
                this.lbError.Text = "Algun campo es invalido";
            }

            var documentNum = long.Parse(doc);

            SelectedClient = this.Clients.Find((c) => c.Document.Equals(documentNum));

            var clientB = new ClientBussiness();

            if (SelectedClient != null)
            {
                var client = SelectedClient;
                client.Email = mail;
                client.Name = name;
                client.Surname = surname;

                clientB.updateOne(client);
            } else
            {
                var client = new Client();
                client.Document = documentNum;
                client.Name = name;
                client.Surname = surname;
                client.Email = mail;

                clientB.saveOne(client);
            }

            loadData();
        }

        protected void rpClients_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            long selectedDoc = long.Parse(e.CommandArgument.ToString());
            
            var clientB = new ClientBussiness();

            SelectedClient = clientB.getAll().Find((c) => c.Document.Equals(selectedDoc));

            this.tbDocument.Text = selectedDoc.ToString();
            this.tbDocument.Enabled = false;
            this.tbMail.Text = SelectedClient.Email;
            this.tbName.Text = SelectedClient.Name;
            this.tbSurname.Text = SelectedClient.Surname;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallOpenModal", "ClickOpenModal()", true);
        }

        protected void btnNewClient_Click(object sender, EventArgs e)
        {
            this.SelectedClient = null;
            this.tbDocument.Enabled = true;

            this.tbDocument.Text = "";
            this.tbMail.Text = "";
            this.tbName.Text = "";
            this.tbSurname.Text = "";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallOpenModal", "ClickOpenModal()", true);
        }
    }
}