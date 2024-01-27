using AutoMapper;
using proyecto_api.Modelos;
using proyecto_api.Modelos.Dto;

namespace proyecto_api
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Proyecto, ProyectoDto>();
            CreateMap<ProyectoDto, Proyecto>();

            CreateMap<Proyecto, ProyectoUpdateDto>().ReverseMap();
            CreateMap<Proyecto, ProyectosDto>().ReverseMap();
        }
    }
}
