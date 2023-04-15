using AutoMapper;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.Application.Features.Areas.Messages.Commands;
using DC.AnimalChipization.WebApi.ViewModels.Areas;
using DC.AnimalChipization.WebApi.ViewModels.Areas.Requests;

namespace DC.AnimalChipization.WebApi.Mappings;

public class AreaProfile : Profile
{
    public AreaProfile()
    {
        CreateMap<AreaDto, AreaViewModel>();
        CreateMap<AreaPointDto, AreaPointViewModel>().ReverseMap();

        CreateMap<CreateAreaRequest, AddAreaCommandMessage>();
        CreateMap<UpdateAreaRequest, UpdateAreaCommandMessage>();
    }
}