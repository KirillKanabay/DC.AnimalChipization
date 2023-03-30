using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DC.AnimalChipization.Application.Features.Accounts.Messages.Commands;
using DC.AnimalChipization.WebApi.ViewModels.Accounts.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DC.AnimalChipization.WebApi.Controllers;

[ApiController]
public class AuthenticationController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AuthenticationController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("/registration")]
    public async Task<ActionResult> Register(RegisterAccountRequest request)
    {
        var command = _mapper.Map<RegisterAccountCommandMessage>(request);
        var result = await _mediator.Send(command);

        return CreatedAtAction
        (
            actionName: AccountsController.ActionGetByIdName,
            controllerName: AccountsController.ControllerName,
            routeValues: new { accountId = result.Id },
            value: result
        );
    }
}