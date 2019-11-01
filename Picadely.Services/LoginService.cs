using Picadely.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Picadely.Services
{
    public class PicadasServices
    {
        public async Task<List<Picada>> GetPicadas()
        {
            var sqlService = new SqlAccessService();

            var dataTable = await sqlService.SelectData("Picadas", new Parameters().Send());

            var picadas = new List<Picada>();
            foreach (var row in dataTable.Rows)
            {
                picadas.Add(new Picada
                {
                    Nombre = dataTable.Rows[0]["Nombre"].ToString(),
                    Comensales = int.Parse(dataTable.Rows[0]["Comensales"].ToString()),
                    Id = int.Parse(dataTable.Rows[0]["Id"].ToString())
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
        public async Task<Usuario> LoginAsync(string email, string password)
        {

            var sqlService = new SqlAccessService();
            var hashService = new HashService();

            var passwordHash = hashService.Hash(password);

            var dataTable = await sqlService.SelectData("Usuarios", new Parameters()
                                                    .Add("Email", email)
                                                    .Add("Password", passwordHash)
                                                    .Send());
            if (dataTable.Rows.Count == 0)
            {
                await sqlService.InsertDataAsync("Logs", new Parameters()
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



            await sqlService.InsertDataAsync("Logs", new Parameters()
                .Add("Tipo", TipoLog.Informacion.ToString())
                .Add("Fecha", DateTime.Now)
                .Add("Email", user.Email)
                .Add("Descripcion", "Login de Usuario"));

            return user;
        }
    }
}
