using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Commands;
using DC.AnimalChipization.Application.Features.AnimalTypes.Messages.Queries;
using DC.AnimalChipization.WebApi.ViewModels.AnimalTypes;
using DC.AnimalChipization.WebApi.ViewModels.AnimalTypes.Requests;

namespace DC.AnimalChipization.WebApi.Controllers;

[ApiController]
[Route("Animals/Types")]
public class AnimalTypesController : ControllerBase
{
    #region [ControllerConstants]

    public const string ControllerName = "AnimalTypes";

    public const string ActionGetByIdName = "GetById";
    public const string ActionCreateName = "Create";
    public const string ActionUpdateName = "Update";
    public const string ActionDeleteName = "Delete";

    #endregion

    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AnimalTypesController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("{typeId:long}")]
    [ActionName(ActionGetByIdName)]
    public async Task<ActionResult<AnimalTypeViewModel>> GetByIdAsync(long typeId)
    {
        var message = new GetAnimalTypeByIdQueryMessage { AnimalTypeId = typeId };
        var locationDto = await _mediator.Send(message);
        var viewModel = _mapper.Map<AnimalTypeViewModel>(locationDto);

        return Ok(viewModel);
    }

    [HttpPost]
    [ActionName(ActionCreateName)]
    public async Task<ActionResult<AnimalTypeViewModel>> CreateAsync([FromBody] CreateAnimalTypeRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        var message = _mapper.Map<AddAnimalTypeCommandMessage>(request);
        var locationDto = await _mediator.Send(message);
        var viewModel = _mapper.Map<AnimalTypeViewModel>(locationDto);

        return CreatedAtAction
        (
            actionName: ActionGetByIdName,
            controllerName: ControllerName,
            routeValues: new { typeId = viewModel.Id },
            value: viewModel
        );
    }

    [HttpPut("{typeId:long}")]
    [ActionName(ActionUpdateName)]
    public async Task<ActionResult<AnimalTypeViewModel>> UpdateAsync(long typeId, [FromBody] UpdateAnimalTypeRequest request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        var message = _mapper.Map<UpdateAnimalTypeCommandMessage>(request);
        message.AnimalTypeId = typeId;
        var locationDto = await _mediator.Send(message);
        var viewModel = _mapper.Map<AnimalTypeViewModel>(locationDto);

        return Ok(viewModel);
    }

    [HttpDelete("{typeId:long}")]
    [ActionName(ActionDeleteName)]
    public async Task<ActionResult> DeleteAsync(long typeId)
    {
        var message = new DeleteAnimalTypeCommandMessage { AnimalTypeId = typeId };
        await _mediator.Send(message);

        return Ok();
    }
}