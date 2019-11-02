using Picadely.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Picadely.Services
{
    public class ComprasServices
    {
        public void Comprar(Usuario usuario, Picada picada)
        {
            var sqlService = new SqlAccessService();

            sqlService.InsertDataAsync("Compras", new Parameters().Add("Usuario_Id", usuario.Id.ToString())
                                                                  .Add("Picada_Id", picada.Id.ToString()));
            sqlService.InsertDataAsync("Logs", new Parameters()
                               .Add("Tipo", TipoLog.Informacion.ToString())
                               .Add("Fecha", DateTime.Now)
                               .Add("Digito", new HashService().Hash(TipoLog.Informacion.ToString() + DateTime.Now + usuario.Email + $"Se realizo una compra de {picada.Nombre}"))
                               .Add("Email", usuario.Email)
                               .Add("Descripcion", $"Se realizo una compra de {picada.Nombre}"));
        }

        public List<DetalleCompra> GetPedidos()
        {
            var sqlService = new SqlAccessService();

            var dataTable = sqlService.SelectData("SELECT u.Nombre, Apellido, Direccion, P.Nombre as PicadaNombre FROm Compras C Inner Join Usuarios U on C.usuario_Id = u.id   INNER JOIN Picadas P on C.Picada_id = P.id");

            var picadas = new List<DetalleCompra>();
            foreach (DataRow row in dataTable.Rows)
            {
                picadas.Add(new DetalleCompra
                {

                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    Direccion = row["Direccion"].ToString(),
                    PicadaNombre = row["PicadaNombre"].ToString()
                });
            }
            return picadas;

        }

    }
}
