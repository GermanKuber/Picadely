<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardCliente.aspx.cs" Inherits="Picadely.UI.DashboardCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Cliente"></asp:Label>
    <asp:DataList ID="DLPicadas" runat="server"></asp:DataList>
</asp:Content>

