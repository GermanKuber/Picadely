<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Picadely.UI.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox TextMode="Email" ID="TxtEmail" Text="cliente@cliente.com" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox TextMode="Password" ID="TxtPassword" Text="12345" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="BtnLogin" runat="server" Text="Button" OnClick="BtnLogin_Click" />
        </div>
        <asp:Label ID="LblError" ForeColor="Red" runat="server" ></asp:Label>
    </form>
</body>
</html>
