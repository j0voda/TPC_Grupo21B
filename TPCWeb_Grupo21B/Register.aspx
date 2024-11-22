<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TPCWeb_Grupo21B.Screens.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main class="container d-flex flex-column gap-3">
        <header>
            <h2>Termina de completar tus datos para poder operar correctamente:</h2>
            <h4 class="text-black-50">Verifica tus datos, en caso de error contacta al usuario admin:</h4>
        </header>
        <section class="d-flex gap-2">
            <div class="flex-grow-1">
                <label class="form-label">Email:</label>
                <input type="text" class="form-control" disabled value="<%: user.Email %>" />
            </div>
            <div class="flex-grow-1">
                <label class="form-label">Document:</label>
                <input type="text" class="form-control" disabled value="<%: user.Documento %>" />
            </div>
            <div class="flex-grow-1">
                <label class="form-label">Genero:</label>
                <input type="text" class="form-control" disabled value="<%: user.Sexo.Equals("M") ? "Hombre" : "Mujer" %>" />
            </div>
            <div class="flex-grow-1">
                <label class="form-label">Rol asignado:</label>
                <input type="text" class="form-control" disabled value="<%: user.Rol.Name %>" />
            </div>
        </section>
        <section class="d-flex flex-column gap-2 align-items-start">
            <h3>Completa tus datos faltantes</h3>
            <section class="d-flex gap-2 w-100">
                <div class="flex-grow-1">
                    <label class="form-label">Nombre:</label>
                    <asp:TextBox ID="tbName" runat="server" placeholder="Juan" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="flex-grow-1">
                    <label class="form-label">Apellido:</label>
                    <asp:TextBox ID="tbSurname" runat="server" placeholder="Proto" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="flex-grow-1">
                    <label class="form-label">Usuario: <span class="text-danger fs-6 fw-light"><%= this.usernameInUse ? "Nombre de usuario invalido" : "" %></span></label>
                    <asp:TextBox ID="tbUsername" runat="server" placeholder="JuanProto" CssClass="form-control" OnTextChanged="tbUsername_TextChanged" AutoPostBack="true"></asp:TextBox>
                </div>
                <div class="flex-grow-1">
                    <label class="form-label">Nueva contraseña:</label>
                    <asp:TextBox ID="tbPassword" runat="server" placeholder="xxx" CssClass="form-control" TextMode="Password"></asp:TextBox>
                </div>
            </section>
            <asp:Button ID="btnSave" runat="server" Text="Terminar registro" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </section>
    </main>
</asp:Content>
