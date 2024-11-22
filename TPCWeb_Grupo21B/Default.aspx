<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPCWeb_Grupo21B._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .urgent_time {
            animation-name: color;
            animation-duration: .5s;
            animation-iteration-count: infinite;
            animation-direction: alternate-reverse;
            animation-timing-function: ease-in;
        }
        @keyframes color {
            to {
                background-color: white;
            }
        }
    </style>
    <main>
        <section class="row mb-4">
            <h2 id="aspnetTitle" class="fs-3">Bienvenido: <%: user.Nombres %></h2>
            <h3 class="fs-5">Rol: <%: user.Rol.Name %></h3>
        </section>
        <section class="mb-4">
            <a href="/TicketCreation" class="btn btn-primary">Crear nueva incidencia</a>
        </section>
        <section class="row">
            <div class="row">
                <div class="col-1">
                    <p>Filtrar por: </p> 
                </div>
                <div class="col-2">
                    <p>ID</p>
                    <asp:TextBox ID="txtFltId" runat="server" AutoPostBack="false" TextMode="Number" CssClass="form-select"></asp:TextBox>
                </div>
                <div class="col-2">
                    <p>Estado</p>
                    <asp:DropDownList ID="ddlFltEst" runat="server" AutoPostBack="false" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-2">
                    <p>Cliente</p>
                    <asp:DropDownList ID="ddlFltCli" runat="server" AutoPostBack="false" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-2">
                    <p>Prioridad</p>
                    <asp:DropDownList ID="ddlFltPrio" runat="server" AutoPostBack="false" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-2">
                    <p>Clasificación</p>
                    <asp:DropDownList ID="ddlFltClas" runat="server" AutoPostBack="false" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-1">
                    <asp:Button ID="btnFiltrar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click"/>
                </div>
            </div>
            <h3 class="">Tus tickets:</h3>
            <table class="table">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Asunto</th>
                        <th scope="col">Descripción</th>
                        <th scope="col">Clasificación</th>
                        <th scope="col">Estado</th>
                        <th scope="col">Asignado a</th>
                        <th scope="col">Prioridad</th>
                        <th scope="col">Creado</th>
                        <th scope="col">Tiempo Restante</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptTickets">
                        <ItemTemplate>
                            <tr class='<%# getClassIfUrgent((DateTime)Eval("CreatedAt"), (Int16)Eval("Prioridad.TimeToSolve")) %>' style='background-color: <%# Eval("Prioridad.Color") %>'>
                                <td><%# Eval("Id") %></td>
                                <td><%# Eval("Asunto") %></td>
                                <td><%# Eval("Descripcion") %></td>
                                <td><%# Eval("Clasificacion.Descripcion") %></td>
                                <td><%# Eval("Estado.Descripcion") %></td>
                                <td><%# getUserName((long)Eval("UserId")) %></td>
                                <td><%# Eval("Prioridad.Descripcion") %></td>
                                <td><%# Eval("CreatedAt") %></td>
                                <td>
                                    <span id="timer_<%# Eval("Id") %>" class="countdown" 
                                        data-created-at='<%# ((DateTime)Eval("CreatedAt")).ToString("yyyy-MM-ddTHH:mm:ssZ") %>' 
                                        data-days="<%# Eval("Prioridad.TimeToSolve") %>">
                                    </span>
                                </td>
                                <td><asp:Button ID="btnVerClick" runat="server" Text="Ver" class="btn btn-primary btn-sm" CommandName="VerInfo" CommandArgument='<%# Eval("Id") %>' OnCommand="btnVerClick_Command"/> </td>
                                <%--<td><button class="btn btn-primary btn-sm">Ver</button></td>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                      
                </tbody>
            </table>
        </section>
    </main>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            // Get de contadores
            const timers = document.querySelectorAll(".countdown");

            timers.forEach(timer => {
                const createdAt = new Date(timer.getAttribute("data-created-at"));
                const timeToSolve = parseInt(timer.getAttribute("data-days")) || 0;

                // Calculo de tiempo restante
                const deadline = new Date(createdAt);
                deadline.setTime(deadline.getTime() + (timeToSolve * 60 * 60 * 1000));

                function updateTimer() {
                    const now = new Date();
                    const diff = deadline - now;

                    if (diff <= 0) {
                        timer.textContent = "Tiempo expirado";
                        timer.style.color = "red";
                        return;
                    }

                    const days = Math.floor(diff / (1000 * 60 * 60 * 24));
                    const hours = Math.floor((diff % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
                    const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
                    const seconds = Math.floor((diff % (1000 * 60)) / 1000);

                    timer.textContent = `${days}d ${hours}h ${minutes}m ${seconds}s`;
                }

                // Actualizar timer cada 1 segundo
                updateTimer();
                setInterval(updateTimer, 1000);
            });
        });
    </script>
</asp:Content>

<%--<% foreach (var ticket in tickets) { %>
       <tr>
           <td><%= ticket.Id %></td>
           <td><%= ticket.Asunto %></td>
           <td><%= ticket.Descripcion %></td>
           <td><%= ticket.Clasificacion.Descripcion %></td>
           <td><%= ticket.Estado.Descripcion %></td>
           <td><%= ticket.UserId %></td>
           <td><%= ticket.Prioridad.Descripcion %></td>
           <td><%= ticket.CreatedAt %></td>
           <td><asp:Button ID="btnVer" runat="server" Text="Ver" class="btn btn-primary btn-sm" CommandArgument="<%= ticket.Id %>" OnClick="btnVer_Click"/> </td>
           <td><button class="btn btn-primary btn-sm">Ver</button></td>
       </tr>
<% } %>--%>