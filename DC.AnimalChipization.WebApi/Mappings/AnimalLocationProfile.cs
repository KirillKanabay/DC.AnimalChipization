using AutoMapper;
using DC.AnimalChipization.Application.Features.AnimalLocations.DataTransfer;
using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Commands;
using DC.AnimalChipization.Application.Features.AnimalLocations.Messages.Queries;
using DC.AnimalChipization.WebApi.ViewModels.AnimalLocations;
using DC.AnimalChipization.WebApi.ViewModels.AnimalLocations.Requests;

namespace DC.AnimalChipization.WebApi.Mappings;

public class AnimalLocationProfile : Profile
{
    public AnimalLocationProfile()
    {
        CreateMap<AnimalLocationDto, AnimalLocationViewModel>()
            .ForMember(x => x.DateTimeOfVisitLocationPoint, opt => opt.MapFrom(x => x.VisitDateTime));

        CreateMap<SearchAnimalLocationRequest, SearchAnimalLocationQueryMessage>();

        CreateMap<UpdateAnimalLocationRequest, UpdateAnimalLocationCommandMessage>();
    }
}