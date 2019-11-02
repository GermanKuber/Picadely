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

        protected void Button1_Click(object sender, EventArgs e)
        {
            new BackupService().Crear();
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            new BackupService().Restore();

        }
    }
}