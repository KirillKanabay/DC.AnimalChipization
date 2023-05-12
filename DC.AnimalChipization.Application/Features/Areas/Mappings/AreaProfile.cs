using AutoMapper;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Features.Areas.Messages.Commands;
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

            CreateMap<AddAreaCommandMessage, AreaDto>();
            CreateMap<UpdateAreaCommandMessage, AreaDto>();

            CreateMap<AddAreaCommandMessage, AreaEntity>();
            CreateMap<UpdateAreaCommandMessage, AreaEntity>();
        }
    }
}
