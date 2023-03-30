using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Messages.Queries
{
    public class GetAccountByIdQueryMessage : IRequest<AccountDto>
    {
        public int AccountId { get; set; }
    }
}
