using System;

namespace Picadely.Entities
{
    public class DetalleCompra
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string PicadaNombre { get; set; }
        public string Digito { get; set; }
        public bool Corrompido { get; set; } = false;
        public int UsuarioId { get; set; }
        public int PicadaId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
