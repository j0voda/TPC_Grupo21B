<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ticket.aspx.cs" Inherits="TPCWeb_Grupo21B.Screens.Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="row">
        <h2 class="mb-3"><%: ticketM.Id %> - <%: ticketM.Asunto %> - <span class="fs-4 text-muted"><%: ticketM.Estado.Descripcion %></span></h2>
        <section class="col d-flex flex-column">
            <div class="mb-4 border-bottom">
                <h4>Datos del cliente:</h4>
                <ul class="list-group list-group-flush">
                    <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Documento:</span> <%: Client.Document %></li>
                    <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Nombre completo:</span> <%: Client.Surname %>, <%: Client.Name %></li>
                    <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Email:</span> <%: Client.Email %></li>
                </ul>
            </div>
            <div class="d-flex flex-column flex-grow-1 gap-3 justify-content-center">
                <h4>Datos del ticket:</h4>
                <ul class="list-group list-group-flush">
                    <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Asunto:</span> <%: ticketM.Asunto %></li>
                    <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Descripcion:</span> <%: ticketM.Descripcion %></li>
                </ul>
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
            </div>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-success" OnClick="btnGuardar_Click" />
        </section>
        <section class="col d-flex flex-column">
            <h3>Observaciones:</h3>
            <div class="flex-grow-1 overflow-auto">
                <% foreach (var o in observacions) { %>
                <div class="border-bottom border-dark-subtle">
                    <h5 class="bold d-inline fs-5"><%: getUserNameById(o.UserId) %></h5> - <span class="text-muted fs-6 fw-lighter fst-italic"><%: o.CreatedAt %></span>
                    <p class="py-2 m-0"><%: o.Observation %></p>
                </div>
                <% } %>
            </div>
            <div class="w-100 d-flex flex-column gap-2">
                <asp:TextBox ID="tbObservation" runat="server" TextMode="MultiLine" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                <asp:Button ID="btnSaveObs" runat="server" Text="Guardar observación" CssClass="btn btn-primary" OnClick="btnSaveObs_Click"/>
            </div>
        </section>
    </main>
</asp:Content>
