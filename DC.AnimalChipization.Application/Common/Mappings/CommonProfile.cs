using AutoMapper;
using DC.AnimalChipization.Application.Common.Messages;
using DC.AnimalChipization.Data.Common.Models;

namespace DC.AnimalChipization.Application.Common.Mappings
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<PagedMessage, Paging>();
        }
    }
}
