<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TicketCreation.aspx.cs" Inherits="TPCWeb_Grupo21B.Screens.TickerCreation" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>CREACION DE TICKET:</h2>
        <div class="d-flex flex-column gap-3 w-25 mx-auto justify-content-center">
            <div>
                <label class="form-label">Breve descripción:</label>
                <input class="form-control" type="text" name="title" />
            </div>
            <div>
                <label class="form-label">Cliente:</label>
                <select class="form-select">
                    <option value="value1">Juan Pedro</option>
                    <option value="value2" selected>Pepito</option>
                    <option value="value3">Alguien más</option>
                </select>
            </div>
            <div>
                <label class="form-label">Prioridad:</label>
                <select class="form-select">
                    <option value="value1">Soporte ténico</option>
                    <option value="value2" selected>Soporte comercial</option>
                    <option value="value3">Reporte de falla</option>
                </select>
            </div>
            <div>
                <label class="form-label">Prioridad:</label>
                <select class="form-select">
                    <option value="value1">Alta</option>
                    <option value="value2" selected>Media</option>
                    <option value="value3">Baja</option>
                </select>
            </div>
            <div>
                <label class="form-label">Descripción detallada:</label>
                <textarea class="form-control" name="description"></textarea>
            </div>
            <button class="btn btn-success">CREAR</button>
        </div>
    </main>
</asp:Content>
