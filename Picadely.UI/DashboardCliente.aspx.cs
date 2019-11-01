using Picadely.Services;
using System;
using System.Threading.Tasks;

namespace Picadely.UI
{
    public partial class DashboardCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var picadasServices = new PicadasServices();
            var picadas =  picadasServices.GetPicadas().GetAwaiter().GetResult();

            DLPicadas.DataSource = picadas;
        }
    }
}