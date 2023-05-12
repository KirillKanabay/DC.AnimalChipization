using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Identity.Attributes;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Messages.Queries
{
    [Authorize]
    public class GetAccountByIdQueryMessage : IRequest<AccountDto>
    {
        public int AccountId { get; set; }
    }
}
