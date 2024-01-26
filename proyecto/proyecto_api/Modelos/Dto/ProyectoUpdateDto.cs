using System.ComponentModel.DataAnnotations;

namespace proyecto_api.Modelos.Dto
{
    public class ProyectoUpdateDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]  
        public double Puntuacion { get; set; }

        [Required]  
        public DateTime FechaPublicacion { get; set; }

        [Required]
        public Boolean Terminado { get; set; }

        [Required]  
        public string Autor {  get; set; }

    }
}
