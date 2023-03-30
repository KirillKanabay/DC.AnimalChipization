using AutoMapper;
using DC.AnimalChipization.Application.Identity.Models;
using DC.AnimalChipization.Data.Entities;

namespace DC.AnimalChipization.Application.Identity.Mappings
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<AccountEntity, ApplicationUser>();
        }
    }
}
