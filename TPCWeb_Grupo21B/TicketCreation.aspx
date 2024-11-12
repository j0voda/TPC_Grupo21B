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
        function ClickOpenModalClas() {
            document.addEventListener("DOMContentLoaded", () => document.getElementById("btn-modal-open-clas").click());
        }
    </script>
    <script>
        function ClickOpenModalPrio() {
            document.addEventListener("DOMContentLoaded", () => document.getElementById("btn-modal-open-prio").click());
        }
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
                <button type="button" class="hidden" hidden id="btn-modal-open-clas" data-bs-toggle="modal" data-bs-target="#createClasificationModal"></button>
                <asp:Button ID="btnNewClasification" runat="server" Text="Nuevo" UseSubmitBehavior="false" CssClass="btn btn-primary" OnClick="btnNewClasification_Click"/>
            </div>
            <div>
                <label class="form-label">Prioridad:</label>
                <asp:DropDownList ID="prioSelect" runat="server" class="form-select" OnSelectedIndexChanged="prioSelect_SelectedIndexChanged"></asp:DropDownList>
                <button type="button" class="hidden" hidden id="btn-modal-open-prio" data-bs-toggle="modal" data-bs-target="#createPriorityModal"></button>
                <asp:Button ID="btnNewPriority" runat="server" Text="Nuevo" UseSubmitBehavior="false" CssClass="btn btn-primary" OnClick="btnNewPriority_Click"/>
            </div>
            <div>
                <label class="form-label">Descripción detallada:</label>
                <asp:TextBox ID="txtDescripcion" runat="server" class="form-control" OnTextChanged="txtDescripcion_TextChanged"></asp:TextBox>
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

    <div class="modal fade" id="createPriorityModal" tabindex="-2" aria-labelledby="createPriorityModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="createPriorityModalLabel">Crear nueva prioridad</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="d-flex flex-column gap-3 mx-auto justify-content-center">
                        <div>
                            <label class="form-label">Descripción: </label>
                            <asp:TextBox ID="txtPrioDesc" runat="server" CssClass="form-control" TextMode="SingleLine"></asp:TextBox>
                        </div>
                        <div class="flex-grow-1">
                            <label class="form-label">Color: </label>
                            <asp:TextBox ID="txtPrioColor" runat="server" CssClass="form-control" TextMode="Color"></asp:TextBox>
                        </div>
                    </div>
                        <asp:Label ID="lbErrorPrio" runat="server" Text=""></asp:Label>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnSavePrio" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnSavePrio_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
