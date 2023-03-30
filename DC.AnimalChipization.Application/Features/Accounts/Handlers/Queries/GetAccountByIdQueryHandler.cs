using AutoMapper;
using DC.AnimalChipization.Application.Common.Exceptions;
using DC.AnimalChipization.Application.Features.Accounts.DataTransfer;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Queries;
using DC.AnimalChipization.Data.Common.UoW;
using MediatR;

namespace DC.AnimalChipization.Application.Features.Accounts.Handlers.Queries;

public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQueryMessage, AccountDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAccountByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AccountDto> Handle(GetAccountByIdQueryMessage request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Accounts.GetByIdAsync(request.AccountId);

        if (entity == null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<AccountDto>(entity);
    }
}