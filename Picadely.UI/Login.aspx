<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Picadely.UI.Login" %>

<!DOCTYPE html>
<link href="Content/main.css" rel="stylesheet" />
<link href="Content/util.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100">
				<form id="form1" runat="server" class="login100-form validate-form">
					<span class="login100-form-title p-b-34">
						Bienvenido a Picadelli
					</span>
                <asp:Label ID="LblError" ForeColor="Red" runat="server" ></asp:Label>
					<div class="wrap-input100 rs1-wrap-input100 validate-input m-b-20" data-validate="Ingrese un usuario válido.">
						  <asp:TextBox TextMode="Email" CssClass="input100" ID="TxtEmail" Text="cliente@cliente.com" runat="server"></asp:TextBox>
                        <span class="focus-input100"></span>
					</div>
					<div class="wrap-input100 rs2-wrap-input100 validate-input m-b-20" data-validate="Ingrese una contraseña.">
            <asp:TextBox TextMode="Password" class="input100" ID="TxtPassword" Text="12345" runat="server"></asp:TextBox>
						
                        <span class="focus-input100"></span>
					</div>
					
					<div class="container-login100-form-btn">
                          <asp:Button ID="BtnLogin" CssClass="login100-form-btn" runat="server" Text="Button" OnClick="BtnLogin_Click" />
					</div>

					
				</form>

				<div class="login100-more" style="background-image: url('images/bg-01.jpg');"></div>
			</div>
		</div>
	</div>
   
</body>
</html>
