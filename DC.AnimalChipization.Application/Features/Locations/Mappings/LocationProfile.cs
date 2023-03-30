using AutoMapper;
using DC.AnimalChipization.Application.Features.Locations.DataTransfer;
using DC.AnimalChipization.Application.Features.Locations.Messages.Commands;
using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Application.Features.Locations.Mappings
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            CreateMap<LocationDto, LocationEntity>()
                .ReverseMap();

            CreateMap<AddLocationCommandMessage, LocationEntity>();
            CreateMap<UpdateLocationCommandMessage, LocationEntity>();
        }
    }
}
