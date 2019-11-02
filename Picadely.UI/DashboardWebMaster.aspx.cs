using Picadely.Entities;
using Picadely.Services;
using System;
using System.Web.UI.WebControls;

namespace Picadely.UI
{
    public partial class DashboardWebMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["UsuarioLogueado"] as Usuario;
            if (usuario.Tipo != UsuarioTipo.WebMaster.ToString())
                Response.Redirect("Login.aspx");
            var logsServices = new LogServices();
            var logs = logsServices.Get();

            GridView.DataSource = logs;
            GridView.DataBind();
        }
    }
}