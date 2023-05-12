using AutoMapper;
using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Queries;
using DC.AnimalChipization.Data.Common.Models;
using DC.AnimalChipization.Data.Common.UoW;
using DC.AnimalChipization.Data.Repositories.Filters;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Handlers.Queries;

public class AccountSearchQueryHandler : IRequestHandler<AccountSearchQueryMessage, List<AccountDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AccountSearchQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<AccountDto>> Handle(AccountSearchQueryMessage request, CancellationToken cancellationToken)
    {
        var filter = _mapper.Map<AccountFilter>(request);
        var paging = _mapper.Map<Paging>(request);

        filter.Include(x => x.Role);

        var entities = await _unitOfWork.Accounts.ListAsync(filter, paging);

        return _mapper.Map<List<AccountDto>>(entities);
    }
}