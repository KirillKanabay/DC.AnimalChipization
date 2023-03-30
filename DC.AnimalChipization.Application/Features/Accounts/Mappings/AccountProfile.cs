using AutoMapper;
using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Queries;
using DC.AnimalChipization.Data.Entities;
using DC.AnimalChipization.Data.Repositories.Filters;

namespace DC.AnimalChipization.Application.Features.Accounts.Mappings
{
    internal class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterAccountCommandMessage, AccountEntity>();
            CreateMap<UpdateAccountCommandMessage, AccountEntity>();

            CreateMap<AccountSearchQueryMessage, AccountFilter>();

            CreateMap<AccountDto, AccountEntity>()
                .ReverseMap();
        }
    }
}
