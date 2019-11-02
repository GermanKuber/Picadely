using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picadely.Entities
{
    public class DetalleCompra
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string PicadaNombre { get; set; }

    }
    public class Picada
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Comensales { get; set; }
    }
    public enum UsuarioTipo
    {
        Admin,
        Cliente,
        WebMaster
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
    public class Log
    {
        public string Tipo { get; set; }
        public string Fecha { get; set; }
        public string Email { get; set; }
        public string Descripcion { get; set; }
        public string Digito { get; set; }
        public bool Corrompido { get; set; } = false;
    }
}
