<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardWebMaster.aspx.cs" Inherits="Picadely.UI.DashboardWebMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <asp:GridView ID="GridView" runat="server">
    </asp:GridView>
    <asp:Button ID="Button2" runat="server" Text="Crear Backup" OnClick="Button1_Click" />
    <asp:Button ID="Button1" runat="server" Text="Restor Backup" OnClick="Button1_Click1" />
</asp:Content>
