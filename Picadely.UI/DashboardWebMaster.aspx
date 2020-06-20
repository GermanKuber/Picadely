<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardWebMaster.aspx.cs" Inherits="Picadely.UI.DashboardWebMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <label>Logs</label>
    <asp:GridView CssClass="table" HeaderStyle-CssClass="thead-dark" ID="GridView" runat="server">
    </asp:GridView>
    <label>Compras</label>
    <asp:GridView CssClass="table" HeaderStyle-CssClass="thead-dark" ID="GdvCompras" runat="server">
    </asp:GridView>
    <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
    <asp:Button ID="Button2" runat="server" Text="Crear Backup" OnClick="Button1_Click" />
    <asp:Button ID="Button1" runat="server" Text="Restor Backup" OnClick="Button1_Click1" />
</asp:Content>
