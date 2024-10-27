<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministracionUsuarios.aspx.cs" Inherits="TPCWeb_Grupo21B.AdministracionUsuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>GESTIÓN DE USUARIOS</h2>
        <section class="mb-4">
            <a href="/TicketCreation" class="btn btn-primary">Crear nueva incidencia</a>
        </section>
        <section class="row">
            <h3 class="">Tus tickets:</h3>
            <table class="table">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Nombre</th>
                        <th scope="col">Apellido</th>
                        <th scope="col">Género</th>
                        <th scope="col">Rol</th>
                        <th scope="col">Creado</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var user in users)
                        { %>
                    <tr>
                        <td><%= user.Id %></td>
                        <td><%= user.Nombres %></td>
                        <td><%= user.Apellidos %></td>
                        <td><%= user.Sexo %></td>
                        <td><%= user.Rol.Name %></td>
                        <td><%= user.CreatedAt.ToString("dd-MM-yyyy") %></td>
                        <td><button class="btn btn-primary btn-sm">Ver</button></td>
                    </tr>
                    <% } %>
                </tbody>
            </table>
        </section>
    </main>
</asp:Content>
