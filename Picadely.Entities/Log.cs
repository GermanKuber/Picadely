namespace Picadely.Entities
{
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
