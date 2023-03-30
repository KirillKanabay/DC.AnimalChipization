using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Queries;
using DC.AnimalChipization.WebApi.ViewModels.Accounts;
using DC.AnimalChipization.WebApi.ViewModels.Accounts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DC.AnimalChipization.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    #region [ControllerConstants]

    public const string ControllerName = "Accounts";
   
    public const string ActionGetByIdName = "GetById";
    public const string ActionSearchName = "Search";
    public const string ActionUpdateName = "Update";
    public const string ActionDeleteName = "Delete";
    #endregion

    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AccountsController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("{accountId:int}")]
    [ActionName(ActionGetByIdName)]
    public async Task<ActionResult<AccountViewModel>> GetByIdAsync(int accountId)
    {
        var message = new GetAccountByIdQueryMessage { AccountId = accountId };
        var accountDto = await _mediator.Send(message);
        var viewModel = _mapper.Map<AccountViewModel>(accountDto);

        return Ok(viewModel);
    }

    [HttpGet("search")]
    [ActionName(ActionSearchName)]
    public async Task<ActionResult<List<AccountViewModel>>> SearchAsync([FromQuery] AccountSearchRequest request)
    {
        var message = _mapper.Map<AccountSearchQueryMessage>(request);
        var accountDtos = await _mediator.Send(message);
        var viewModels = _mapper.Map<List<AccountViewModel>>(accountDtos);

        return Ok(viewModels);
    }

    [HttpPut("{accountId:int}")]
    [ActionName(ActionUpdateName)]
    public async Task<ActionResult<AccountViewModel>> UpdateAsync(int accountId, [FromBody] UpdateAccountRequest request)
    {
        var message = _mapper.Map<UpdateAccountCommandMessage>(request);
        message.AccountId = accountId;
        var accountDto = await _mediator.Send(message);
        var viewModel = _mapper.Map<AccountViewModel>(accountDto);

        return Ok(viewModel);
    }

    [HttpDelete("{accountId:int}")]
    [ActionName(ActionDeleteName)]
    public async Task<ActionResult<AccountViewModel>> DeleteAsync(int accountId)
    {
        var message = new DeleteAccountCommandMessage { AccountId = accountId };
        await _mediator.Send(message);

        return Ok();
    }
}