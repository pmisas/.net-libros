﻿using System.ComponentModel.DataAnnotations;

namespace proyecto_api.Modelos.Dto
{
    public class ProyectoDto
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public double Puntuacion { get; set; }

        public DateTime FechaPublicacion { get; set; }

        [Required]
        public Boolean Terminado { get; set; }

        public string Autor {  get; set; }

    }
}
