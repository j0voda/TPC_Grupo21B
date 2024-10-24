<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPCWeb_Grupo21B._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row mb-4">
            <h2 id="aspnetTitle" class="fs-3">Bienvenido: <%: user.Nombres %></h2>
            <h3 class="fs-5">Rol: <%: (user.RolId == 1 ? "Telefonista" : (user.RolId == 2 ? "Supervisor" : "Administrador")) %></h3>
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
                        <th scope="col">Estado</th>
                        <th scope="col">Asignado</th>
                        <th scope="col">Prioridad</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">1</th>
                        <td>Ticket de prueba</td>
                        <td>Abierto</td>
                        <td>Tú</td>
                        <td>Baja</td>
                        <td><button class="btn btn-primary btn-sm">IR A VER</button></td>
                    </tr>
                    <tr>
                        <th scope="row">2</th>
                        <td>Ticket de prueba</td>
                        <td>Abierto</td>
                        <td>Tú</td>
                        <td>Baja</td>
                        <td><button class="btn btn-primary btn-sm">IR A VER</button></td>
                    </tr>
                    <tr>
                        <th scope="row">3</th>
                        <td>Ticket de prueba</td>
                        <td>Abierto</td>
                        <td>Tú</td>
                        <td>Baja</td>
                        <td><button class="btn btn-primary btn-sm">IR A VER</button></td>
                    </tr>
                </tbody>
            </table>
        </section>
    </main>

</asp:Content>
