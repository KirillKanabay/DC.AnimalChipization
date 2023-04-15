using AutoMapper;
using DC.AnimalChipization.Application.Features.Areas.DataTransfer;
using DC.AnimalChipization.WebApi.ViewModels.Areas;

namespace DC.AnimalChipization.WebApi.Mappings;

public class AreaProfile : Profile
{
    public AreaProfile()
    {
        CreateMap<AreaDto, AreaViewModel>();
        CreateMap<AreaPointDto, AreaPointViewModel>().ReverseMap();
    }
}