<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPCWeb_Grupo21B.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <%--<% if (this.HasError) { %>
        <div class="alert alert-danger" role="alert">
            Ah ocurrido un error inesperado. Por favor volver a comenzar el proceso.
        </div>
        <% } %>--%>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">Call Center</h1>
            <p class="lead">Ingrese usuario y contraseña</p>
        </section>
        <asp:Label runat="server" CssClass="control-label mb-1" AssociatedControlID="username">Usuario:</asp:Label>
        <asp:TextBox ID="username" runat="server" CssClass="form-control" ClientIDMode="Static" OnTextChanged="username_TextChanged"></asp:TextBox>

        <asp:Label runat="server" CssClass="control-label mb-1" AssociatedControlID="password">Contraseña:</asp:Label>
        <asp:TextBox ID="password" runat="server" CssClass="form-control" ClientIDMode="Static" OnTextChanged="password_TextChanged"></asp:TextBox>

        <asp:Button ID="btnNext" runat="server" Text="Ingresar" CssClass="btn btn-primary mt-2" OnClick="btnNext_Click" />

        <asp:Label ID="lblError" runat="server" Text="" CssClass="invalid-feedback"></asp:Label>
    </main>
</asp:Content>
