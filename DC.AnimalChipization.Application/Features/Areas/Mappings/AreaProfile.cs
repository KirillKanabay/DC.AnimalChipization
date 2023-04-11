using AutoMapper;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Application.Features.Areas.Mappings
{
    public class AreaProfile : Profile
    {
        public AreaProfile()
        {
            CreateMap<AreaEntity, AreaDto>()
                .ReverseMap();
            
            CreateMap<AreaPointEntity, AreaPointDto>()
                .ReverseMap();
        }
    }
}
