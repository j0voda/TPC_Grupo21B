<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ticket.aspx.cs" Inherits="TPCWeb_Grupo21B.Screens.Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>Ticket Nro: <%: ticketM.Id %></h2>
        <div class="d-flex flex-column gap-3 w-25 mx-auto justify-content-center">
            <div>
                <label class="form-label">Asunto:</label>
                <asp:TextBox ID="txtAsunto" runat="server" class="form-control" OnTextChanged="txtAsunto_TextChanged"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Asignado a:</label>
                <asp:DropDownList ID="userSelect" runat="server" class="form-select" OnSelectedIndexChanged="userSelect_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div>
                <label class="form-label">Nro Cliente:</label>
                <asp:TextBox ID="txtCliente" runat="server" class="form-control" OnTextChanged="txtCliente_TextChanged"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Clasificación:</label>
                <asp:DropDownList ID="clasSelect" runat="server" class="form-select" OnSelectedIndexChanged="clasSelect_SelectedIndexChanged"></asp:DropDownList>
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
