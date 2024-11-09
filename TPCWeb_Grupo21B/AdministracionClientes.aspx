<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdministracionClientes.aspx.cs" Inherits="TPCWeb_Grupo21B.AdministracionClientes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function ClickOpenModal() {
            document.addEventListener("DOMContentLoaded", () => document.getElementById("btn-modal-open").click());
        }
    </script>
    <main>
        <h2>Gestión de clientes</h2>
        <section class="mb-4">
            <button type="button" class="hidden" hidden id="btn-modal-open" data-bs-toggle="modal" data-bs-target="#createUserModal"></button>
            <asp:Button 
                ID="btnNewClient" 
                runat="server" 
                Text="Crear nuevo cliente"
                UseSubmitBehavior="false"
                CssClass="btn btn-primary"
                OnClick="btnNewClient_Click"
            />
        </section>
        <section class="row">
            <h3 class="">Clientes existentes:</h3>
            <table class="table">
                <thead>
                    <tr>
                        <th scope="col">Documento</th>
                        <th scope="col">Nombre</th>
                        <th scope="col">Apellido</th>
                        <th scope="col">Mail</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpClients" runat="server" OnItemCommand="rpClients_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Document") %></td>
                                <td><%# Eval("Name") %></td>
                                <td><%# Eval("Surname") %></td>
                                <td><%# Eval("Email") %></td>
                                <td>
                                    <asp:Button
                                        ID="btnEdit"
                                        runat="server"
                                        Text="Editar"
                                        CssClass="btn btn-primary btn-sm"
                                        UseSubmitBehavior="false"
                                        CommandArgument='<%# Eval("Document") %>'
                                        />
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
                    <h1 class="modal-title fs-5" id="createUserModalLabel">Dar de alta un nuevo cliente</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="d-flex flex-column gap-3 mx-auto justify-content-center">
                        <div>
                            <label class="form-label">Número de documento:</label>
                            <asp:TextBox ID="tbDocument" runat="server" CssClass="form-control" TextMode="Number" required="true"></asp:TextBox>
                        </div>
                        <div class="d-flex gap-3 flex-grow-1">
                            <div>
                                <label class="form-label">Nombre:</label>
                                <asp:TextBox ID="tbName" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                            </div>
                            <div class="flex-grow-1">
                                <label class="form-label">Apellido:</label>
                                <asp:TextBox ID="tbSurname" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <label class="form-label">Mail:</label>
                            <asp:TextBox ID="tbMail" runat="server" CssClass="form-control" TextMode="Email" required="true"></asp:TextBox>
                        </div>
                        <p class="fs-6 fw-light">Una vez dado de alta le llegara un mail informado al cliente.</p>
                        <asp:Label ID="lbError" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                    <asp:Button ID="btnSaveUser" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnSaveUser_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
