<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TicketCreation.aspx.cs" Inherits="TPCWeb_Grupo21B.Screens.TicketCreation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script defer>
        document.addEventListener("DOMContentLoaded", () => {
            const select = document.querySelector("#client-combo > select");
            const text = document.querySelector("#client-combo > input");

            let open = false;

            select.addEventListener("click", (e) => {
                if (!open) {
                    open = true;
                } else {
                    console.log(e.target.selectedOptions[0]);
                    const option = e.target.selectedOptions[0];

                    if (!option) return;

                    text.value = option.value;
                }
            })

            select.addEventListener("blur", () => {
                open = false;
            })
        });
    </script>
    <main>
        <h2>NUEVO TICKET:</h2>
        <div class="d-flex flex-column gap-3 w-25 mx-auto justify-content-center">
            <div>
                <label class="form-label">Asunto:</label>
                <asp:TextBox ID="txtAsunto" runat="server" class="form-control" OnTextChanged="txtAsunto_TextChanged"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Nro Cliente:</label>
                <div id="client-combo">
                    <asp:TextBox ID="txtCliente" runat="server" class="form-control border-bottom-0 rounded-top rounded-0" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:DropDownList ID="ddCliente" runat="server" SkinID="client-select-combo" CssClass="form-select rounded-bottom rounded-0"></asp:DropDownList>
                </div>
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
            <asp:Button ID="btnCrear" runat="server" Text="Crear" class="btn btn-success" OnClick="btnCrear_Click"/>
        </div>
    </main>
</asp:Content>
