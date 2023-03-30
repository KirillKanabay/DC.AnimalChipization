using AutoMapper;
using DC.AnimalChipization.Application.Features.AnimalTypes.DataTransfer;
using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;
using DC.AnimalChipization.WebApi.ViewModels.AnimalTypes;
using DC.AnimalChipization.WebApi.ViewModels.AnimalTypes.Requests;

namespace DC.AnimalChipization.WebApi.Mappings;

public class AnimalTypeProfile : Profile
{
    public AnimalTypeProfile()
    {
        CreateMap<AnimalTypeDto, AnimalTypeViewModel>();

        CreateMap<CreateAnimalTypeRequest, AddAnimalTypeCommandMessage>();
        CreateMap<UpdateAnimalTypeRequest, UpdateAnimalTypeCommandMessage>();
    }
}