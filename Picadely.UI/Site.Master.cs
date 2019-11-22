using Picadely.Entities;
using Picadely.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Picadely.UI
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var comprasService = new ComprasServices();
            if (!comprasService.IsValid())
                throw new Exception("Error de datos");

            var usuario = Session["UsuarioLogueado"] as Usuario;
            if (usuario == null)
                Response.Redirect("Login.aspx");
        }
    }
}