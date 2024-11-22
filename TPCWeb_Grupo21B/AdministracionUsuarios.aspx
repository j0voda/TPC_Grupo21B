<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministracionUsuarios.aspx.cs" Inherits="TPCWeb_Grupo21B.AdministracionUsuarios" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function ClickOpenModal() {
            document.addEventListener("DOMContentLoaded", () => document.getElementById("btn-modal-open").click());
        }

        const isUserSeleted = Boolean.parse('<%= (openModal).ToString() %>');
        if (isUserSeleted) ClickOpenModal();
    </script>
    <main>
        <h2>Gestión de usuarios</h2>
        <section class="mb-4">
            <button type="button" hidden id="btn-modal-open" data-bs-toggle="modal" data-bs-target="#createUserModal"></button>
            <asp:Button ID="btnNewUser" runat="server" Text="Crear nuevo usuario" CssClass="btn btn-primary" OnClick="btnNewUser_Click" UseSubmitBehavior="false"/>
        </section>
        <section class="row">
            <h3 class="">Usuarios existentes:</h3>
            <table class="table">
                <thead>
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Nombre</th>
                        <th scope="col">Apellido</th>
                        <th scope="col">Género</th>
                        <th scope="col">Mail</th>
                        <th scope="col">Rol</th>
                        <%--<th scope="col">Estado</th>--%>
                        <th scope="col">Creado</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpUsers" runat="server" OnItemCommand="rpUsers_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Id") %></td>
                                <td><%# Eval("Nombres") %></td>
                                <td><%# Eval("Apellidos") %></td>
                                <td><%# Eval("Sexo") %></td>
                                <td><%# Eval("Email") %></td>
                                <td><%# Eval("Rol.Name") %></td>
                                <td><%# ((DateTime)Eval("CreatedAt")).ToString("dd-MM-yyyy") %></td>
                                <td>
                                    <asp:Button
                                        ID="btnEdit"
                                        runat="server"
                                        Text="Editar"
                                        CssClass="btn btn-primary btn-sm"
                                        UseSubmitBehavior="false"
                                        CommandArgument='<%# Eval("Id") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </section>
    </main>
    <div class="modal fade" id="createUserModal" tabindex="-1" aria-labelledby="createUserModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="createUserModalLabel">Crear un usuario</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="d-flex flex-column gap-3 mx-auto justify-content-center">
                        <div>
                            <label class="form-label">Mail:</label>
                            <asp:TextBox ID="tbMail" runat="server" CssClass="form-control" TextMode="Email" required="true"></asp:TextBox>
                        </div>
                        <div class="d-flex gap-3">
                            <div class="flex-grow-1">
                                <label class="form-label">Número de documento:</label>
                                <asp:TextBox ID="tbDocument" runat="server" CssClass="form-control" TextMode="Number" required="true"></asp:TextBox>
                            </div>
                            <div class="flex-grow-1">
                                <label class="form-label">Género:</label>
                                <asp:DropDownList ID="sltSex" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div>
                            <label class="form-label">Rol del nuevo usuario:</label>
                            <asp:DropDownList ID="sltRole" runat="server" CssClass="form-select"></asp:DropDownList>
                        </div>
                        <p class="fs-6 fw-light">Una vez confirme se enviara un mail con el código para que el usuario termine el registro.</p>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <% if (selectedUser != null) { %>
                    <asp:Button ID="btnDelete" runat="server" Text="Borrar usuario" OnClick="btnDelete_Click" CssClass="btn btn-danger" />
                    <% } %>
                    <asp:Button ID="btnSaveUser" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnSaveUser_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
