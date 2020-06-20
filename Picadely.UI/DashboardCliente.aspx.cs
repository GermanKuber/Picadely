using Picadely.Entities;
using Picadely.Services;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Picadely.UI
{
    public partial class DashboardCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["UsuarioLogueado"] as Usuario;
            if (usuario == null ||
                usuario.Tipo != UsuarioTipo.Cliente.ToString())
                Response.Redirect("Login.aspx");

            var picadasServices = new PicadasServices();
            var picadas = picadasServices.GetPicadas();

            var comensales = picadasServices.GetComensalesFromXml();
            TxtComensales.Text = string.Join(", ", comensales.Select(i => i.ToString()).ToArray());

            GridView.DataSource = picadas;
            GridView.DataBind();
            UserId.Value = usuario.Id.ToString();
        }

        protected void GridView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var picadasServices = new PicadasServices();
            var picadas = picadasServices.GetPicadas();
            var index = int.Parse(e.CommandArgument.ToString());
            var selectedPicada = picadas.ElementAt(index);
            var compraService = new ComprasServices();

            var usuario = Session["UsuarioLogueado"] as Usuario;
            compraService.Comprar(usuario, selectedPicada);
        }
    }
}