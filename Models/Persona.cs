namespace ReportesMVC.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ApPaterno { get; set; } = string.Empty;
        public string ApMaterno { get; set; } = string.Empty; // Inicializado a cadena vacía
        public int IIdSexo { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string TelefonoOCelular1 { get; set; } = string.Empty;
        public int IIdTipoDocumento { get; set; }
        public string NumeroIdentificacion { get; set; } = string.Empty;
    }
}