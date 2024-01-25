using proyecto_api.Modelos.Dto;

namespace proyecto_api.Datos
{
    public static class ProyectoStore
    {
        public static List<ProyectoDto> proyectoList = new List<ProyectoDto>
        {
            new ProyectoDto{Id=1, Nombre="Cien años de soledad", Autor="Amanda", Descripcion="ninguna", Terminado=true, Puntuacion=5},
            new ProyectoDto {Id=2,Nombre="Barbie", Autor="Amanda", Descripcion="ninguna1", Terminado=false, Puntuacion=0},
        };
    }
}
