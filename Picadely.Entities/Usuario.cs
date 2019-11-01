using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picadely.Entities
{
    public class Picada
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Comensales { get; set; }
    }
    public enum UsuarioTipo
    {
        Admin,
        Cliente
    }
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Tipo { get; set; }
        public string Direccion { get; set; }
    }
}
