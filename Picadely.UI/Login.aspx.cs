using Picadely.Entities;
using Picadely.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Picadely.UI
{

    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected  void BtnLogin_Click(object sender, EventArgs e)
        {
            var email = TxtEmail.Text;
            var password = TxtPassword.Text;

            var loginService = new LoginService();


            var user =  loginService.LoginAsync(email, password);
            if (user == null)
            {
                LblError.Text = "Error al ingresar passwor o email";
            }
            else
            {
                Session["UsuarioLogueado"] = user;

                if (user.Tipo == UsuarioTipo.Admin.ToString())
                    Response.Redirect("~/Dashboard.aspx");
                else if (user.Tipo == UsuarioTipo.Cliente.ToString())
                    Response.Redirect("~/DashboardCliente.aspx");
                else if (user.Tipo == UsuarioTipo.WebMaster.ToString())
                    Response.Redirect("~/DashboardWebMaster.aspx");

            }
        }
    }
}