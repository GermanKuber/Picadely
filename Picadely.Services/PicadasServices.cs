using Picadely.Entities;
using System.Collections.Generic;
using System.Data;

namespace Picadely.Services
{
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

    public class LogServices
    {
        public List<Log> Get()
        {
            var sqlService = new SqlAccessService();

            var dataTable = sqlService.SelectDatas("Logs", new List<string> { "Id", "Tipo", "Fecha", "Email", "Descripcion","Digito" });

            var logs = new List<Log>();
            foreach (DataRow row in dataTable.Rows)
            {
                logs.Add(new Log
                {

                    Email = row["Email"].ToString(),
                    Descripcion = row["Descripcion"].ToString(),
                    Fecha = row["Fecha"].ToString(),
                    Digito = row["Digito"].ToString(),
                    Tipo = row["Tipo"].ToString(),
                });
            }
            return logs;

        }
    }
}
