namespace ReportesMVC.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        public required string Correo { get; set; }
        public required string Contrasena { get; set; }
    }
}
