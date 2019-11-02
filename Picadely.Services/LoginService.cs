using Picadely.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

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
                               .Add("Email", usuario.Email)
                               .Add("Descripcion", $"Se realizo una compra de {picada.Nombre}"));
        }

    }
    public class PicadasServices
    {
        public List<Picada> GetPicadas()
        {
            var sqlService = new SqlAccessService();

            var dataTable = sqlService.SelectDatas("Picadas", new List<string> { "Id", "Nombre", "Comensales" });

            var picadas = new List<Picada>();
            foreach (DataRow row in dataTable.Rows)
            {
                picadas.Add(new Picada
                {

                    Nombre = row["Nombre"].ToString(),
                    Comensales = int.Parse(row["Comensales"].ToString()),
                    Id = int.Parse(row["Id"].ToString())
                });
            }
            return picadas;

        }
    }
    public enum TipoLog
    {
        Error,
        Alerta,
        Informacion
    }
    public class LoginService
    {
        public Usuario LoginAsync(string email, string password)
        {

            var sqlService = new SqlAccessService();
            var hashService = new HashService();

            var passwordHash = hashService.Hash(password);

            var dataTable = sqlService.SelectData("Usuarios", new Parameters()
                                                    .Add("Email", email)
                                                    .Add("Password", passwordHash)
                                                    .Send());
            if (dataTable.Rows.Count == 0)
            {
                sqlService.InsertDataAsync("Logs", new Parameters()
                                 .Add("Tipo", TipoLog.Alerta.ToString())
                                 .Add("Fecha", DateTime.Now)
                                 .Add("Email", email)
                                 .Add("Descripcion", "Intento de login fallido"));
                return null;
            }
            var user = new Usuario
            {
                Apellido = dataTable.Rows[0]["Apellido"].ToString(),
                Email = dataTable.Rows[0]["Email"].ToString(),
                Nombre = dataTable.Rows[0]["Nombre"].ToString(),
                Tipo = dataTable.Rows[0]["Tipo"].ToString(),
                Direccion = dataTable.Rows[0]["Direccion"].ToString(),
                Id = int.Parse(dataTable.Rows[0]["Id"].ToString()),
            };



            sqlService.InsertDataAsync("Logs", new Parameters()
               .Add("Tipo", TipoLog.Informacion.ToString())
               .Add("Fecha", DateTime.Now)
               .Add("Email", user.Email)
               .Add("Descripcion", "Login de Usuario"));

            return user;
        }
    }
}
