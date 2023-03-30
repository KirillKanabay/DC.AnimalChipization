using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DC.AnimalChipization.Application.Features.Animals.Messages.Commands;
using DC.AnimalChipization.Application.Features.Animals.Messages.Queries;
using DC.AnimalChipization.WebApi.ViewModels.Animals;
using DC.AnimalChipization.WebApi.ViewModels.Animals.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DC.AnimalChipization.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalsController : ControllerBase
{
    #region [ControllerConstants]

    public const string ControllerName = "Animals";

    public const string ActionGetByIdName = "GetById";
    public const string ActionSearchName = "Search";
    public const string ActionCreateName = "Create";
    public const string ActionUpdateName = "Update";
    public const string ActionDeleteName = "Delete";
    public const string ActionAppendAnimalTypeName = "AppendAnimalType";
    public const string ActionChangeAnimalTypeName = "ChangeAnimalType";
    public const string ActionRemoveAnimalTypeName = "RemoveAnimalType";

    #endregion

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AnimalsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{animalId:long}")]
    [ActionName(ActionGetByIdName)]
    public async Task<ActionResult<AnimalViewModel>> GetByIdAsync(long animalId)
    {
        var message = new GetAnimalByIdQueryMessage { AnimalId = animalId };
        var animalDto = await _mediator.Send(message);
        var viewModel = _mapper.Map<AnimalViewModel>(animalDto);

        return Ok(viewModel);
    }

    [HttpGet("search")]
    [ActionName(ActionSearchName)]
    public async Task<ActionResult<List<AnimalViewModel>>> SearchAsync([FromQuery] AnimalSearchRequest request)
    {
        var message = _mapper.Map<AnimalSearchQueryMessage>(request);
        var response = await _mediator.Send(message);
        var viewModels = _mapper.Map<List<AnimalViewModel>>(response);

        return Ok(viewModels);
    }

    [HttpPost]
    [ActionName(ActionCreateName)]
    public async Task<ActionResult<AnimalViewModel>> CreateAsync([FromBody] CreateAnimalRequest request)
    {
        var message = _mapper.Map<AddAnimalCommandMessage>(request);
        var response = await _mediator.Send(message);
        var viewModel = _mapper.Map<AnimalViewModel>(response);

        return CreatedAtAction
        (
            actionName: ActionGetByIdName,
            controllerName: ControllerName,
            routeValues: new { animalId = viewModel.Id },
            viewModel
        );
    }

    [HttpPut("{animalId:long}")]
    [ActionName(ActionUpdateName)]
    public async Task<ActionResult<AnimalViewModel>> UpdateAsync(long animalId, [FromBody] UpdateAnimalRequest request)
    {
        var message = _mapper.Map<UpdateAnimalCommandMessage>(request);
        message.AnimalId = animalId;
        var response = await _mediator.Send(message);
        var viewModel = _mapper.Map<AnimalViewModel>(response);

        return Ok(viewModel);
    }

    [HttpDelete("{animalId:long}")]
    [ActionName(ActionDeleteName)]
    public async Task<ActionResult> DeleteAsync(long animalId)
    {
        var message = new DeleteAnimalCommandMessage { AnimalId = animalId };
        await _mediator.Send(message);

        return Ok();
    }

    [HttpPost("{animalId:long}/types/{typeId:long}")]
    [ActionName(ActionAppendAnimalTypeName)]
    public async Task<ActionResult<AnimalViewModel>> AppendAnimalTypeAsync(long animalId, long typeId)
    {
        var message = new AppendAnimalTypeCommandMessage
        {
            AnimalId = animalId,
            TypeId = typeId
        };
        var response = await _mediator.Send(message);
        var viewModel = _mapper.Map<AnimalViewModel>(response);

        return CreatedAtAction
        (
            actionName: ActionGetByIdName,
            controllerName: ControllerName,
            routeValues: new { animalId = viewModel.Id },
            viewModel
        );
    }

    [HttpPut("{animalId:long}/types")]
    [ActionName(ActionChangeAnimalTypeName)]
    public async Task<ActionResult<AnimalViewModel>> ChangeAnimalTypeAsync(long animalId, [FromBody] ChangeAnimalTypeRequest request)
    {
        var message = _mapper.Map<ChangeAnimalTypeCommandMessage>(request);
        message.AnimalId = animalId;

        var response = await _mediator.Send(message);
        var viewModel = _mapper.Map<AnimalViewModel>(response);

        return Ok(viewModel);
    }

    [HttpDelete("{animalId:long}/types/{typeId:long}")]
    [ActionName(ActionRemoveAnimalTypeName)]
    public async Task<ActionResult<AnimalViewModel>> RemoveAnimalTypeAsync(long animalId, long typeId)
    {
        var message = new RemoveAnimalTypeCommandMessage
        {
            AnimalId = animalId,
            TypeId = typeId
        };
        var response = await _mediator.Send(message);
        var viewModel = _mapper.Map<AnimalViewModel>(response);

        return Ok(viewModel);
    }
}