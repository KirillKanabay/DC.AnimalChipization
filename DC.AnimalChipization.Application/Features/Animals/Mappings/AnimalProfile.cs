using AutoMapper;
using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using DC.AnimalChipization.Application.Features.Animals.Messages.Queries;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;

namespace DC.AnimalChipization.Application.Features.Animals.Mappings
{
    internal class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            CreateMap<AddAnimalCommandMessage, AnimalEntity>()
                .ForMember(x => x.AnimalTypes, opt => opt.Ignore());

            CreateMap<AnimalEntity, AnimalDto>()
                .ReverseMap();

            CreateMap<AnimalSearchQueryMessage, AnimalFilter>();

            CreateMap<UpdateAnimalCommandMessage, AnimalDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.AnimalId));
        }
    }
}
