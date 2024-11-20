<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ticket.aspx.cs" Inherits="TPCWeb_Grupo21B.Screens.Ticket" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        document.addEventListener("DOMContentLoaded", () => {
            const $textFinalMessage = document.querySelector(".taFinalMessage");
            const $closeButton = document.querySelector(".btnCerrarTicket");

            //Check para asegurar que se guarda un mensaje
            $textFinalMessage.addEventListener("input", (e) => {
                if (e.currentTarget.value != "") {
                    $closeButton.disabled = false;
                } else {
                    $closeButton.disabled = true;
                }
            })
        });
    </script>
    <main class="row">
        <header class="w-100 d-flex justify-content-between">
            <h2 class="mb-3"><%: ticketM.Id %> - <%: ticketM.Asunto %> - <span class="fs-4 text-muted"><%: ticketM.Estado.Descripcion %></span></h2>
            <% if (ticketM.Estado.Id == (int)dominio.Estado.STATES_CODES.CLOSE || ticketM.Estado.Id == (int)dominio.Estado.STATES_CODES.SOLVED)
                { %>
            <asp:Button ID="btnReAbrir" runat="server" Text="Reabrir" CssClass="btn btn-warning" OnClick="btnReAbrir_Click" />
            <% }
                else
                {
            %>
            <%--<asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CssClass="btn btn-danger" OnClick="btnCerrar_Click"/>--%>
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#closeTicket">
                Cerrar ticket
            </button>
            <% } %>
        </header>
        <section class="col d-flex flex-column">
            <div class="mb-4 border-bottom">
                <h4>Datos del cliente:</h4>
                <ul class="list-group list-group-flush">
                    <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Documento:</span> <%: Client.Document %></li>
                    <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Nombre completo:</span> <%: Client.Surname %>, <%: Client.Name %></li>
                    <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Email:</span> <%: Client.Email %></li>
                </ul>
            </div>
            <div class="d-flex flex-column flex-grow-1 gap-3">
                <h4>Datos del ticket:</h4>
                <ul class="list-group list-group-flush">
                    <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Asunto:</span> <%: ticketM.Asunto %></li>
                    <li class="list-group-item d-flex justify-content-between"><span class="fw-bold">Descripcion:</span> <%: ticketM.Descripcion %></li>
                </ul>
                <div>
                    <label class="form-label">Asignado a:</label>
                    <asp:DropDownList ID="userSelect" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <div>
                    <label class="form-label">Clasificación:</label>
                    <asp:DropDownList ID="clasSelect" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <div>
                    <label class="form-label">Prioridad:</label>
                    <asp:DropDownList ID="prioSelect" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
            </div>
        </section>
        <section class="col d-flex flex-column">
            <h3>Observaciones:</h3>
            <div class="flex-grow-1 overflow-auto">
                <% foreach (var o in observacions)
                    { %>
                <div class="border-bottom border-dark-subtle">
                    <h5 class="bold d-inline fs-5"><%: getUserNameById(o.UserId) %></h5>
                    - <span class="text-muted fs-6 fw-lighter fst-italic"><%: o.CreatedAt %></span>
                    <p class="py-2 m-0"><%: o.Observation %></p>
                </div>
                <% } %>
            </div>
            <div class="w-100 d-flex flex-column gap-2">
                <asp:TextBox ID="tbObservation" runat="server" TextMode="MultiLine" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                <asp:Button ID="btnSaveObs" runat="server" Text="Guardar observación" CssClass="btn btn-primary" OnClick="btnSaveObs_Click" />
            </div>
        </section>
    </main>
    <div class="modal fade" id="closeTicket" tabindex="-1" aria-labelledby="closeTicketModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="closeTicketModalLabel">Seguro desea cerrar el ticket?</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="d-flex flex-column gap-3 mx-auto justify-content-center">
                        <div>
                            <label class="form-label">Por favor deja un mesaje final:</label>
                            <textarea id="tbFinalMessage" runat="server" class="form-control taFinalMessage" placeholder="Se resolvió por garantía"></textarea>
                        </div>
                        <div class="form-check">
                            <input class="form-check-input" runat="server" type="checkbox" id="checkSolved" />
                            <label id="lbSolved" class="form-check-label" for="MainContent_checkSolved">El problema fue solucionado?</label>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar ticket" CssClass="btn btn-danger btnCerrarTicket" OnClick="btnCerrar_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
