<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardCliente.aspx.cs" Inherits="Picadely.UI.DashboardCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Cliente"></asp:Label>
    <asp:GridView CssClass="table" HeaderStyle-CssClass="thead-dark" ID="GridView" runat="server" OnRowCommand="GridView_RowCommand"    OnSelectedIndexChanged="GridView_SelectedIndexChanged">
        <Columns>
            <asp:ButtonField Text="Comprar"  />
        </Columns>
    </asp:GridView>
</asp:Content>

