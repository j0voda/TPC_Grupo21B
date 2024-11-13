<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ticket.aspx.cs" Inherits="TPCWeb_Grupo21B.Screens.Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main">
        <h2 class="mb-3">Ticket: <%: ticketM.Id %> - <%: ticketM.Asunto %></h2>
        <div class="w-50 mx-auto mb-4 border-bottom">
            <h4>Datos del cliente:</h4>
            <ul class="list-group list-group-flush">
                <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Documento:</span> <%: Client.Document %></li>
                <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Nombre completo:</span> <%: Client.Surname %>, <%: Client.Name %></li>
                <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Email:</span> <%: Client.Email %></li>
            </ul>
        </div>
        <div class="d-flex flex-column gap-3 w-50 mx-auto justify-content-center">
            <h4>Datos del ticket:</h4>
            <div>
                <label class="form-label">Asignado a:</label>
                <asp:DropDownList ID="userSelect" runat="server" CssClass="form-select" OnSelectedIndexChanged="userSelect_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div>
                <label class="form-label">Clasificación:</label>
                <asp:DropDownList ID="clasSelect" runat="server" CssClass="form-select" OnSelectedIndexChanged="clasSelect_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div>
                <label class="form-label">Prioridad:</label>
                <asp:DropDownList ID="prioSelect" runat="server" class="form-select" OnSelectedIndexChanged="prioSelect_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div>
                <label class="form-label">Descripción detallada:</label>
                <asp:TextBox ID="txtDescripcion" runat="server" class="form-control" OnTextChanged="txtDescripcion_TextChanged"></asp:TextBox>
            </div>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-success" OnClick="btnGuardar_Click"/>
        </div>
    </main>
</asp:Content>
