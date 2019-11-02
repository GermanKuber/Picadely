using Picadely.Entities;
using System;
using System.Threading.Tasks;

namespace Picadely.Services
{
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
                                 .Add("Digito", hashService.Hash(TipoLog.Alerta.ToString() + DateTime.Now + email + "Intento de login fallido"))
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
               .Add("Digito", hashService.Hash(TipoLog.Informacion.ToString() + DateTime.Now + email + "Login de Usuario"))
               .Add("Descripcion", "Login de Usuario"));

            return user;
        }
    }
}
