using AutoMapper;
using DC.AnimalChipization.Application.Features.AnimalLocations.DataTransfer;
using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands;
using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Queries;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;

namespace DC.AnimalChipization.Application.Features.AnimalLocations.Mappings
{
    internal class AnimalLocationProfile : Profile
    {
        public AnimalLocationProfile()
        {
            CreateMap<AnimalLocationEntity, AnimalLocationDto>()
                .ReverseMap();

            CreateMap<AddAnimalLocationCommandMessage, AnimalLocationEntity>()
                .ForMember(x => x.LocationPointId, opt => opt.MapFrom(x => x.PointId));

            CreateMap<SearchAnimalLocationQueryMessage, AnimalLocationFilter>();

            CreateMap<UpdateAnimalLocationCommandMessage, AnimalLocationEntity>();
        }
    }
}
