<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPCWeb_Grupo21B._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row mb-4">
            <h2 id="aspnetTitle" class="fs-3">Bienvenido: <%: user.Nombres %></h2>
            <h3 class="fs-5">Rol: <%: user.Rol.Name %></h3>
        </section>
        <section class="mb-4">
            <a href="/TicketCreation" class="btn btn-primary">Crear nueva incidencia</a>
        </section>
        <section class="row">
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
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                      <% foreach (var ticket in tickets) { %>
                             <tr>
                                 <td><%= ticket.Id %></td>
                                 <td><%= ticket.Asunto %></td>
                                 <td><%= ticket.Descripcion %></td>
                                 <td><%= ticket.Clasificacion.Descripcion %></td>
                                 <td><%= ticket.Estado.Descripcion %></td>
                                 <td><%= ticket.UserId %></td>
                                 <td><%= ticket.Prioridad.Descripcion %></td>
                                 <td><%= ticket.CreatedAt %></td>
                                 <td><button class="btn btn-primary btn-sm">Ver</button></td>
                             </tr>
                      <% } %>
                </tbody>
            </table>
        </section>
    </main>

</asp:Content>
