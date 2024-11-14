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
                    const option = e.target.selectedOptions[0];

                    if (!option) return;

                    text.value = option.value;

                    const event = new Event('change');

                    text.dispatchEvent(event);
                }
            })

            select.addEventListener("blur", () => {
                open = false;
            })
        });
    </script>
    <script>
        document.addEventListener("DOMContentLoaded", () => {
            const classifModal = document.getElementById("createClasificationModal");

            classifModal?.addEventListener("hidden.bs.modal", () => {
                classifModal.querySelector('input[type="text"]').value = "";
                classifModal.querySelector('span').innerText = "";
            })

        });
    </script>
    <main>
        <h2>NUEVO TICKET:</h2>
        <div class="d-flex flex-column gap-3 w-50 mx-auto justify-content-center">
            <div>
                <label class="form-label">Asunto:</label>
                <asp:TextBox ID="txtAsunto" runat="server" class="form-control" OnTextChanged="txtAsunto_TextChanged"></asp:TextBox>
            </div>
            <div>
                <label class="form-label">Nro Cliente:</label>
                <div id="client-combo">
                    <asp:TextBox ID="txtCliente" runat="server" class="form-control border-bottom-0 rounded-top rounded-0" OnTextChanged="txtCliente_TextChanged" AutoPostBack="true" placeholder="Buscar cliente..."></asp:TextBox>
                    <asp:DropDownList ID="ddCliente" runat="server" SkinID="client-select-combo" CssClass="form-select rounded-bottom rounded-0"></asp:DropDownList>
                </div>
            </div>
            <div>
                <label class="form-label">Clasificación:</label>
                <div class="d-flex gap-1">
                    <asp:DropDownList ID="clasSelect" runat="server" class="form-select" OnSelectedIndexChanged="clasSelect_SelectedIndexChanged"></asp:DropDownList>
                    <button type="button" id="btn-modal-open-clas" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#createClasificationModal">Agregar</button>
                </div>
            </div>
            <div>
                <label class="form-label">Prioridad:</label>
                <asp:DropDownList ID="prioSelect" runat="server" class="form-select" OnSelectedIndexChanged="prioSelect_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div>
                <label class="form-label">Descripción detallada:</label>
                <asp:TextBox ID="txtDescripcion" runat="server" class="form-control" OnTextChanged="txtDescripcion_TextChanged" TextMode="MultiLine"></asp:TextBox>
            </div>
            <asp:Button ID="btnCrear" runat="server" Text="Crear" class="btn btn-success" OnClick="btnCrear_Click"/>
        </div>
    </main>

    <div class="modal fade" id="createClasificationModal" tabindex="-1" aria-labelledby="createClasificationModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="createClasificationModalLabel">Crear nueva clasificación</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="d-flex flex-column gap-3 mx-auto justify-content-center">
                        <div>
                            <label class="form-label">Descripción: </label>
                            <asp:TextBox ID="txtClasDesc" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                        </div>
                        <asp:Label ID="lbErrorClas" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnSaveClasif" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnSaveClasif_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
