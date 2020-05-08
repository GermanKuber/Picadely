<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardCliente.aspx.cs" Inherits="Picadely.UI.DashboardCliente" %>

<%@ Import Namespace="Picadely.Entities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">


        function comprar(id) {
            var userId = parseInt($("#MainContent_UserId").val());
            fetch('https://localhost:44392/api/picadely/', {
                method: 'POST', // or 'PUT'
                body: JSON.stringify({
                    PicadaId: id,
                    UsuarioId: userId
                }), // data can be `string` or {object}!
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(res => res.json())
                .catch(error => console.error('Error:', error))
                .then(response => alert("Acaba de realizar el pedido"));
        }
    </script>
    <asp:Label ID="Label1" runat="server" Text="Cliente"></asp:Label>
    <asp:HiddenField runat="server" ID="UserId" />
    <asp:GridView CssClass="table" HeaderStyle-CssClass="thead-dark" ID="GridView" runat="server" OnRowCommand="GridView_RowCommand" OnSelectedIndexChanged="GridView_SelectedIndexChanged">
        <Columns>


            <asp:TemplateField HeaderText="Id">
                <ItemTemplate>
                    <input type="button" class="btn btn-primary" onclick="comprar(<%#Eval("Id")%>)" value="Comprar" data-picada-id="<%#Eval("Id")%>" />

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

