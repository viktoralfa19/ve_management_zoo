using AutoMapper;
using ZooDto;
using ZooService.Models;

namespace ZooService.Automapper
{
    /// <summary>
    /// Mapper from entities to Dto's and the other way around
    /// </summary>
    /// <seealso cref="Profile" />
    public class ZooProfileMap : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZooProfileMap"/> class.
        /// </summary>
        public ZooProfileMap()
        {
            CreateMap<Animal, ZooAnimalDto>()
                .ForMember(dto => dto.TypeName, conf => conf.MapFrom(entity => entity.AnimalType.Name))
                .ForMember(dto => dto.TypeId, conf => conf.MapFrom(entity => entity.IdAnimalType));
            CreateMap<ZooAnimalRegisterDto, Animal>();
            CreateMap<ZooAnimalDto, ZooAnimalRegisterDto>()
                .ForMember(dto => dto.IdAnimalType, conf => conf.MapFrom(dto => dto.TypeId));
        }
    }
}
