using AutoMapper;
using DC.AnimalChipization.Application.Features.Locations.DataTransfer;
using DC.AnimalChipization.Application.Features.Locations.Messages.Commands;
using DC.AnimalChipization.WebApi.ViewModels.Locations;
using DC.AnimalChipization.WebApi.ViewModels.Locations.Requests;

namespace DC.AnimalChipization.WebApi.Mappings;

public class LocationsProfile : Profile
{
    public LocationsProfile()
    {
        CreateMap<LocationDto, LocationViewModel>();

        CreateMap<CreateLocationRequest, AddLocationCommandMessage>();
        CreateMap<UpdateLocationRequest, UpdateLocationCommandMessage>();
    }
}