using AutoMapper;
using TPFINALFINANZAS.DTOs;
using TPFINALFINANZAS.Models;

namespace TPFINALFINANZAS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- Mapeos para Gasto (Ya definidos) ---
            CreateMap<GastoCreacionDto, Gasto>();
            CreateMap<Gasto, GastoDto>()
                .ForMember(dest => dest.NombreCategoria, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nombre : "Sin Categoría"))
                .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario != null ? src.Usuario.Nombre : "Sin Usuario"));

            // --- Mapeos para CATEGORIA (Nuevos) ---

            // Entidad (DB) a DTO (Vista/Listado)
            CreateMap<Categoria, CategoriaDto>();

            // DTO (Creación) a Entidad (DB)
            CreateMap<CategoriaCreacionDto, Categoria>();

            // Para el caso de edición (opcional, si tienes un DTO de edición separado)
            // CreateMap<Categoria, CategoriaCreacionDto>(); 
        }
    }
}