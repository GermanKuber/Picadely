using Picadely.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Picadely.Services
{
    public class ComprasServices
    {
        private SqlAccessService sqlService = new SqlAccessService();
        public void Comprar(Usuario usuario, Picada picada)
        {


            sqlService.InsertDataAsync("Compras", new Parameters().Add("Usuario_Id", usuario.Id.ToString())
                                                                  .Add("Picada_Id", picada.Id.ToString())
                                                                  .Add("Fecha", DateTime.Now.Date)
                                                                  .Add("Digito", new HashService().Hash(usuario.Id.ToString() + picada.Id.ToString() + DateTime.Now.Date)));
            sqlService.InsertDataAsync("Logs", new Parameters()
                               .Add("Tipo", TipoLog.Informacion.ToString())
                               .Add("Fecha", DateTime.Now)
                               .Add("Digito", new HashService().Hash(TipoLog.Informacion.ToString() + DateTime.Now.Date + usuario.Email + $"Se realizo una compra de {picada.Nombre}"))
                               .Add("Email", usuario.Email)
                               .Add("Descripcion", $"Se realizo una compra de {picada.Nombre}"));

            var hashes = sqlService.SelectData("select Digito from Compras");
            var completo = string.Empty;
            foreach (DataRow item in hashes.Rows)
                completo = completo + item["Digito"];
            var codigoHashCompleto = new HashService().Hash(completo);

            var verificable = sqlService.SelectData("SELECT Id,Codigo From Verificable");
            if (verificable.Rows.Count == 0)
                sqlService.InsertDataAsync("Verificable", new Parameters().Add("Codigo", codigoHashCompleto));
            else
                sqlService.UpdateAsync("Verificable", new Parameters().Add("Codigo", codigoHashCompleto), new Parameters().Add("Id", int.Parse(verificable.Rows[0]["Id"].ToString())));
        }
        public bool IsValid()
        {
            var hashes = sqlService.SelectData("select Digito from Compras");
            var completo = string.Empty;
            foreach (DataRow item in hashes.Rows)
                completo = completo + item["Digito"];
            var codigoHashCompleto = new HashService().Hash(completo);

            var verificable = sqlService.SelectData("SELECT Id,Codigo From Verificable");
            if (verificable.Rows.Count == 0)
                return true;
            else
                if (verificable.Rows[0]["Codigo"].ToString() == codigoHashCompleto)
                return true;

            return false;
        }

        public List<DetalleCompra> GetPedidos()
        {
            var sqlService = new SqlAccessService();

            var dataTable = sqlService.SelectData("SELECT u.Nombre, Apellido, Direccion, P.Nombre as PicadaNombre, C.Digito, C.Fecha, U.Id as UsuarioId, C.Picada_id as PicadaId FROm Compras C Inner Join Usuarios U on C.usuario_Id = u.id   INNER JOIN Picadas P on C.Picada_id = P.id");

            var picadas = new List<DetalleCompra>();
            foreach (DataRow row in dataTable.Rows)
            {
                picadas.Add(new DetalleCompra
                {

                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    Direccion = row["Direccion"].ToString(),
                    PicadaNombre = row["PicadaNombre"].ToString(),
                    Digito = row["Digito"].ToString(),
                    UsuarioId = int.Parse(row["UsuarioId"].ToString()),
                    PicadaId = int.Parse(row["PicadaId"].ToString()),
                    Fecha = DateTime.Parse(row["Fecha"].ToString()),

                });
            }
            return picadas;

        }

    }
}
