using AutoMapper;
using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Queries;
using DC.AnimalChipization.WebApi.ViewModels.Accounts;
using DC.AnimalChipization.WebApi.ViewModels.Accounts.Requests;

namespace DC.AnimalChipization.WebApi.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterAccountRequest, RegisterAccountCommandMessage>();
            CreateMap<UpdateAccountRequest, UpdateAccountCommandMessage>();
            CreateMap<CreateAccountRequest, AddAccountCommandMessage>();
            CreateMap<AccountSearchRequest, AccountSearchQueryMessage>();
            CreateMap<AccountViewModel, AccountDto>().ReverseMap();
        }
    }
}
