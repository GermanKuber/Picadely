using Picadely.Entities;
using Picadely.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Picadely.UI
{
    public partial class DashboardWebMaster : System.Web.UI.Page
    {
        private List<Log> _log;
        protected void Page_Load(object sender, EventArgs e)
        {
            var usuario = Session["UsuarioLogueado"] as Usuario;
            if (usuario.Tipo != UsuarioTipo.WebMaster.ToString())
                Response.Redirect("Login.aspx");
            var logsServices = new LogServices();
             _log = logsServices.Get();
            var hashServices = new HashService();
            foreach (var log in _log)
            {
                var hash = hashServices.Hash(log.Tipo + log.Fecha + log.Email + log.Descripcion);
                if (hash != log.Digito)
                    log.Corrompido = true;
            }
            GridView.DataSource = _log;
            GridView.DataBind();

            var comprasServices = new ComprasServices();
            var compras = comprasServices.GetPedidos();
            foreach (var compa in compras)
            {
                var hash = hashServices.Hash(compa.UsuarioId.ToString() +  compa.PicadaId.ToString() + compa.Fecha.Date.ToString());
                if (hash != compa.Digito)
                    compa.Corrompido = true;
            }
            GdvCompras.DataSource = compras;
            GdvCompras.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            new BackupService().Crear();
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            new BackupService().Restore();

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            var fecha = Calendar1.SelectedDate.ToShortDateString();
            GridView.DataSource = _log.Where(x => x.Fecha.Contains(fecha));
            GridView.DataBind();
        }
    }
}