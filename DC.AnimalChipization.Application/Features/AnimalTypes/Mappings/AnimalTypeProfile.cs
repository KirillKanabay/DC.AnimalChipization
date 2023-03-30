using AutoMapper;
using DC.AnimalChipization.Application.Features.AnimalTypes.DataTransfer;
using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;
using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Application.Features.AnimalTypes.Mappings;

public class AnimalTypeProfile : Profile
{
    public AnimalTypeProfile()
    {
        CreateMap<AnimalTypeEntity, AnimalTypeDto>()
            .ReverseMap();

        CreateMap<AddAnimalTypeCommandMessage, AnimalTypeEntity>();
        CreateMap<UpdateAnimalTypeCommandMessage, AnimalTypeEntity>();
    }
}