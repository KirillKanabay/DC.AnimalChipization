using System.Linq;
using AutoMapper;
using DC.AnimalChipization.Application.Features.Animals.DataTransfer;
using DC.AnimalChipization.Application.Features.Animals.Enums;
using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using DC.AnimalChipization.Application.Features.Animals.Messages.Queries;
using DC.AnimalChipization.WebApi.Immutable;
using DC.AnimalChipization.WebApi.ViewModels.Animals;
using DC.AnimalChipization.WebApi.ViewModels.Animals.Requests;

namespace DC.AnimalChipization.WebApi.Mappings;

public class AnimalProfile : Profile
{
    public AnimalProfile()
    {
        CreateMap<CreateAnimalRequest, AddAnimalCommandMessage>()
            .ForMember(x => x.Gender, opt => opt.MapFrom(x => Genders.ConvertToEnum(x.Gender)));

        CreateMap<AnimalDto, AnimalViewModel>()
            .ForMember(x => x.AnimalTypes, opt => opt.MapFrom(x => x.AnimalTypes.Select(at => at.Id).OrderBy(at => at)))
            .ForMember(x => x.VisitedLocations, opt => opt.MapFrom(x => x.VisitedLocations.Select(vl => vl.Id)))
            .ForMember(x => x.Gender, opt => opt.MapFrom(x => Genders.ConvertToString(x.Gender)))
            .ForMember(x => x.LifeStatus, opt => opt.MapFrom(x => LifeStatuses.ConvertToString(x.LifeStatus)));

        CreateMap<AnimalSearchRequest, AnimalSearchQueryMessage>()
            .ForMember(x => x.LifeStatus, opt => opt.MapFrom(x => GetNullableLifeStatus(x.LifeStatus)))
            .ForMember(x => x.Gender, opt => opt.MapFrom(x => GetNullableGender(x.Gender)));

        CreateMap<UpdateAnimalRequest, UpdateAnimalCommandMessage>()
            .ForMember(x => x.Gender, opt => opt.MapFrom(x => Genders.ConvertToEnum(x.Gender)))
            .ForMember(x => x.LifeStatus, opt => opt.MapFrom(x => LifeStatuses.ConvertToEnum(x.LifeStatus)));

        CreateMap<ChangeAnimalTypeRequest, ChangeAnimalTypeCommandMessage>();
    }

    private LifeStatus? GetNullableLifeStatus(string lifeStatus)
    {
        if(string.IsNullOrWhiteSpace(lifeStatus)) return null;

        return LifeStatuses.ConvertToEnum(lifeStatus);
    }

    private Gender? GetNullableGender(string gender)
    {
        if (string.IsNullOrWhiteSpace(gender)) return null;

        return Genders.ConvertToEnum(gender);
    }
}