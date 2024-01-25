using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyecto_api.Modelos
{
    public class Proyecto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public double Puntuacion { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public Boolean Terminado { get; set; }

        public string Autor { get; set; }

    }
}
